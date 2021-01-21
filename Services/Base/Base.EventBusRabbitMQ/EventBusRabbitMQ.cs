using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Base.Infrastructure.EventBus;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using Serilog;

namespace Base.EventBusRabbitMQ
{
    public class EventBusRabbitMQ : IEventsBus
    {
        private readonly ILogger _logger;
        private readonly IRabbitMQPersistentConnection _persistentConnection;
        private readonly int _retryCount;
        private readonly Policy _policy;
        private readonly List<HandlerSubscription> _handlers;
        private IModel _channel;
        private const string dead_letter_exchange = "dead-letter-exchange";
        private const string dead_letter_queue = "dead-letter-queue";
        private readonly string _exchangeName;
        private readonly string _queueName;
        private IModel _consumerChannel;

        public EventBusRabbitMQ(ILogger logger, IRabbitMQPersistentConnection persistentConnection, string exchangeName, string queueName, int retryCount = 5)
        {
            _logger = logger;
            _persistentConnection = persistentConnection;
            _exchangeName = exchangeName;
            _queueName = queueName;
            _policy = Policy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (ex, time) =>
                    {
                        _logger.Warning(ex,
                            "Could not publish event: {EventId} after {Timeout}s ({ExceptionMessage})",
                            $"{time.TotalSeconds:n1}", ex.Message);
                    });
            _handlers = new List<HandlerSubscription>();

            CreateChannel();
            SetupDeadLetterQueue();
            SetupExchange();
            SetupQueue();

            _consumerChannel = CreateConsumerChannel();
        }
        public void CreateChannel()
        {
            if (!_persistentConnection.IsConnected)
            {
                throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");
            }
            if (_channel == null)
            {
                _channel = _persistentConnection.CreateModel();
            }
        }

        public void SetupDeadLetterQueue()
        {
            _channel.ExchangeDeclare(dead_letter_exchange, "fanout", true);
            _channel.QueueDeclare(dead_letter_queue,
                true,
                false,
                false,
                null);
            _channel.QueueBind(dead_letter_queue, dead_letter_exchange, "");
        }

        public void SetupExchange()
        {
            _channel.ExchangeDeclare(_exchangeName, "direct", true);

        }
        public void SetupQueue()
        {
            if (!string.IsNullOrEmpty(_queueName))
            {
                _channel.QueueDeclare(_queueName,
                    true,
                    false,
                    false,
                    new ConcurrentDictionary<string, object>(new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>("x-dead-letter-exchange", dead_letter_exchange) }));
            }
        }

        public void Dispose()
        {

        }

        public void Publish<T>(T @event) where T : IntegrationEvent
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            var eventName = @event.GetType().FullName;

            _logger.Information("Creating RabbitMQ channel to publish event: {EventId} ({EventName})", @event.Id,
                eventName);

            using (var channel = _persistentConnection.CreateModel())
            {
                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);

                _policy.Execute(() =>
                {
                    var properties = channel.CreateBasicProperties();
                    properties.DeliveryMode = 2; // persistent
                    channel.BasicPublish(_exchangeName,
                        eventName,
                        basicProperties: properties,
                        body: body,
                        mandatory: false);
                });
            }
        }

        public void Subscribe<T>(IIntegrationEventHandler<T> handler) where T : IntegrationEvent
        {
            var subAlreadyExists = _handlers.FirstOrDefault(x => x.Handler == handler);
            if (subAlreadyExists == null)
            {
                if (!_persistentConnection.IsConnected)
                {
                    _policy.Execute(context => _persistentConnection.TryConnect(), new Dictionary<string, object>
                    {
                        {
                            "logmessage",
                            "RabbitMQ Client could not connect "
                        }
                    });
                }

                using (var channel = _persistentConnection.CreateModel())
                {
                    var eventName = typeof(T).FullName;

                    _handlers.Add(new HandlerSubscription(handler, typeof(T), eventName));

                    channel.QueueBind(_queueName,
                        _exchangeName,
                        eventName);
                    StartBasicConsume();
                }

            }
        }

        private void StartBasicConsume()
        {
            _logger.Information("Starting RabbitMQ basic consume");

            if (_consumerChannel != null)
            {
                var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

                consumer.Received += Consumer_Received;

                _consumerChannel.BasicConsume(
                    _queueName,
                    false,
                    consumer);
            }
            else
            {
                _logger.Information("StartBasicConsume can't call on _consumerChannel == null");
            }
        }

        private IModel CreateConsumerChannel()
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            _logger.Information("Creating RabbitMQ consumer channel");

            var channel = _persistentConnection.CreateModel();

            channel.CallbackException += (sender, ea) =>
            {
                _logger.Information(ea.Exception, "Recreating RabbitMQ consumer channel");

                _consumerChannel.Dispose();
                _consumerChannel = CreateConsumerChannel();
                StartBasicConsume();
            };

            return channel;
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
        {
            var eventName = eventArgs.RoutingKey;
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            try
            {
                if (message.ToLowerInvariant().Contains("throw-fake-exception"))
                {
                    throw new InvalidOperationException($"Fake exception requested: \"{message}\"");
                }

                var integrationEventHandlers = _handlers.Where(x => x.EventName == eventName).ToList();

                foreach (var integrationEventHandler in integrationEventHandlers)
                {
                    var request = JsonConvert.DeserializeObject(message, integrationEventHandler.EventType) as IntegrationEvent;

                    var success = await TryProcessEvent(request, eventName, integrationEventHandler);

                    if (success)
                    {
                        _consumerChannel.BasicAck(eventArgs.DeliveryTag, false);
                    }
                    else
                    {
                        _consumerChannel.BasicNack(eventArgs.DeliveryTag, false, false);

                    }
                }

            }
            catch (Exception ex)
            {
                _logger.Information(ex, "----- ERROR Processing message \"{Message}\"", message);
            }
        }

        private async Task<bool> TryProcessEvent<T>(T integrationEvent, string eventName, HandlerSubscription handlerSubscription) where T : IntegrationEvent
        {
            try
            {
                var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(handlerSubscription.Handler.GetType().GetGenericArguments().First());

                return await (Task<bool>)concreteType.GetMethod("Handle")?.Invoke(handlerSubscription.Handler, new object[] { integrationEvent });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void StartConsuming()
        {
            throw new NotImplementedException();
        }

        internal class HandlerSubscription
        {
            public HandlerSubscription(IIntegrationEventHandler handler, Type eventType, string eventName)
            {
                Handler = handler;
                EventType = eventType;
                EventName = eventName;
            }

            public IIntegrationEventHandler Handler { get; }

            public Type EventType { get; }

            public string EventName { get; }
        }
    }
}


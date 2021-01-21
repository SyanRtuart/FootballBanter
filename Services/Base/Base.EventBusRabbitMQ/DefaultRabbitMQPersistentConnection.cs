using System;
using System.IO;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;

namespace Base.EventBusRabbitMQ
{
    public class DefaultRabbitMQPersistentConnection
          : IRabbitMQPersistentConnection
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILogger _logger;
        private readonly int _retryCount;
        private IConnection _connection;
        private bool _disposed;
        private IModel _channel;


        private readonly object sync_root = new object();


        public DefaultRabbitMQPersistentConnection(ConnectionDetails connectionDetails, ILogger logger)
        {
            _connectionFactory = CreateFactory(connectionDetails);
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            TryConnect();
        }

        public IConnectionFactory CreateFactory(ConnectionDetails connectionDetails)
        {
            var factory = new ConnectionFactory
            {
                HostName = connectionDetails.EventBusConnection,
                DispatchConsumersAsync = true,
                UserName = connectionDetails.EventBusUserName,
                Password = connectionDetails.EventBusPassword,
                Port = connectionDetails.EventBusPort
            };
            return factory;
        }

       
        public bool IsConnected => _connection != null && _connection.IsOpen && !_disposed;


        public IModel CreateModel()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");
            }

            return _connection.CreateModel();
        }

        public void Dispose()
        {
            if (_disposed) return;

            _disposed = true;

            try
            {
                _connection.Dispose();
            }
            catch (IOException ex)
            {
                _logger.Fatal(ex.ToString());
            }
        }

        public bool TryConnect()
        {
            _logger.Information("RabbitMQ Client is trying to connect");

            lock (sync_root)
            {
                _connection = _connectionFactory.CreateConnection();

                if (IsConnected)
                {
                    _connection.ConnectionShutdown += OnConnectionShutdown;
                    _connection.CallbackException += OnCallbackException;
                    _connection.ConnectionBlocked += OnConnectionBlocked;

                    _logger.Information("RabbitMQ Client acquired a persistent connection to '{HostName}' and is subscribed to failure events", _connection.Endpoint.HostName);

                    return true;
                }

                _logger.Fatal("FATAL ERROR: RabbitMQ connections could not be created and opened");

                return false;
            }
        }

        private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
        {
            if (_disposed) return;

            _logger.Warning("A RabbitMQ connection is shutdown. Trying to re-connect...");

            TryConnect();
        }

        private void OnCallbackException(object sender, CallbackExceptionEventArgs e)
        {
            if (_disposed) return;

            _logger.Warning("A RabbitMQ connection throw exception. Trying to re-connect...");

            TryConnect();
        }

        private void OnConnectionShutdown(object sender, ShutdownEventArgs reason)
        {
            if (_disposed) return;

            _logger.Warning("A RabbitMQ connection is on shutdown. Trying to re-connect...");

            TryConnect();
        }
    }
}

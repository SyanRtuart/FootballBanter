using Autofac;
using Base.Infrastructure.EventBus;
using Serilog;
using UserAccess.IntegrationEvents;

namespace Phrases.Infrastructure.Configuration.EventsBus
{
    public class EventsBusStartup
    {
        public static void Initialize(ILogger logger)
        {
            SubscribeToIntegrationEvents(logger);
        }

        private static void SubscribeToIntegrationEvents(ILogger logger)
        {
            var eventBus = PhrasesCompositionRoot.BeginLifetimeScope().Resolve<IEventsBus>();

            SubscribeToIntegrationEvent<NewUserRegisteredIntegrationEvent>(eventBus, logger);
        }

        private static void SubscribeToIntegrationEvent<T>(IEventsBus eventBus, ILogger logger)
            where T : IntegrationEvent
        {
            logger.Information("Subscribe to {@IntegrationEvent}", typeof(T).FullName);
            eventBus.Subscribe(new IntegrationEventGenericHandler<T>());
        }
    }
}

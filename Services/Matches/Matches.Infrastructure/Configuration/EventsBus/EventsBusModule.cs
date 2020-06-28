using Autofac;
using Base.EventBus;
using Base.Infrastructure.EventBus;

namespace Matches.Infrastructure.Configuration.EventsBus
{
    internal class EventsBusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InMemoryEventBusClient>()
                .As<IEventsBus>()
                .SingleInstance();
        }
    }
}
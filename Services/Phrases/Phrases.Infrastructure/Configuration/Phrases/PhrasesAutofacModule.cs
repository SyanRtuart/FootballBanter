using Autofac;
using Phrases.Application.Contracts;

namespace Phrases.Infrastructure.Configuration.Phrases
{
    public class PhrasesAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PhrasesModule>()
                .As<IPhrasesModule>()
                .InstancePerLifetimeScope();
        }
    }
}
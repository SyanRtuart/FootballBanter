using Autofac;
using Matches.Application.Contracts;

namespace Matches.Infrastructure.Configuration.Match
{
    public class MatchAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MatchModule>()
                .As<IMatchModule>()
                .InstancePerLifetimeScope();
        }
    }
}
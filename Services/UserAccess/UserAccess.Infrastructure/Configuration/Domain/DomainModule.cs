using Autofac;
using UserAccess.Application.UserRegistrations;
using UserAccess.Domain.UserRegistrations;

namespace UserAccess.Infrastructure.Configuration
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UsersCounter>()
                .As<IUsersCounter>()
                .InstancePerLifetimeScope();
        }
    }
}
using Autofac;
using Base.Application.BuildingBlocks;
using Base.Application.Emails;
using Base.Infrastructure.Emails;
using Serilog;
using Serilog.Extensions.Logging;
using UserAccess.Infrastructure.Configuration.DataAccess;
using UserAccess.Infrastructure.Configuration.Email;
using UserAccess.Infrastructure.Configuration.EventsBus;
using UserAccess.Infrastructure.Configuration.Logging;
using UserAccess.Infrastructure.Configuration.Mediator;
using UserAccess.Infrastructure.Configuration.Processing;
using UserAccess.Infrastructure.Configuration.UserAccess;

namespace UserAccess.Infrastructure.Configuration
{
    public class UserAccessStartup
    {
        public static void Initialize(string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger,
            EmailsConfiguration emailsConfiguration,
            string textEncryptionKey,
            IEmailSender emailSender,
            ContainerBuilder builder)
        {
            var moduleLogger = logger.ForContext("Module", "UserAccess");

            ConfigureCompositionRoot(connectionString,
                executionContextAccessor,
                logger,
                emailsConfiguration,
                textEncryptionKey,
                emailSender,
                builder);
        }

        private static void ConfigureCompositionRoot(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger,
            EmailsConfiguration emailsConfiguration,
            string textEncryptionKey,
            IEmailSender emailSender,
            ContainerBuilder builder)
        {
            builder.RegisterModule(new LoggingModule(logger.ForContext("Module", "UserAccess")));

            var loggerFactory = new SerilogLoggerFactory(logger);

            builder.RegisterModule(new UserAccessAutofacModule());
            builder.RegisterModule(new DataAccessModule(connectionString, loggerFactory));
            builder.RegisterModule(new DomainModule());
            builder.RegisterModule(new ProcessingModule());
            builder.RegisterModule(new EventsBusModule());
            builder.RegisterModule(new MediatorModule());
            //containerBuilder.RegisterModule(new OutboxModule());
            builder.RegisterModule(new EmailModule(emailsConfiguration, emailSender));

            builder.RegisterInstance(executionContextAccessor);
        }
    }
}
using System.Collections.Specialized;
using Autofac;
using Autofac.Extras.Quartz;
using Base.Application.BuildingBlocks;
using Base.Application.Emails;
using Base.Infrastructure.Emails;
using Base.Infrastructure.EventBus;
using Serilog;
using Serilog.Extensions.Logging;
using UserAccess.Infrastructure.Configuration.DataAccess;
using UserAccess.Infrastructure.Configuration.Domain;
using UserAccess.Infrastructure.Configuration.Email;
using UserAccess.Infrastructure.Configuration.EventsBus;
using UserAccess.Infrastructure.Configuration.Logging;
using UserAccess.Infrastructure.Configuration.Mediator;
using UserAccess.Infrastructure.Configuration.Processing;
using UserAccess.Infrastructure.Configuration.Processing.Outbox;
using UserAccess.Infrastructure.Configuration.Quartz;
using UserAccess.Infrastructure.Configuration.UserAccess;

namespace UserAccess.Infrastructure.Configuration
{
    public class UserAccessStartup
    {
        private static IContainer _container;

        public static void Initialize(string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger,
            EmailsConfiguration emailsConfiguration,
            string textEncryptionKey,
            IEmailSender emailSender,
            IEventsBus eventsBus,
            bool runQuartz = true
            )
        {
            var moduleLogger = logger.ForContext("Module", "UserAccess");

            ConfigureCompositionRoot(connectionString, executionContextAccessor, moduleLogger, emailsConfiguration, eventsBus, emailSender, runQuartz);


            if (runQuartz)
            {
                QuartzStartup.Initialize(moduleLogger);
            }

            //ToDo Initialize event bus here
        }

        private static void ConfigureCompositionRoot(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger,
            EmailsConfiguration emailsConfiguration,
            IEventsBus eventsBus,
            IEmailSender emailSender,
            bool runQuartz = true)
        {
            var builder = new ContainerBuilder();


            builder.RegisterModule(new LoggingModule(logger.ForContext("Module", "UserAccess")));

            var loggerFactory = new SerilogLoggerFactory(logger);

            //builder.RegisterModule(new UserAccessAutofacModule());
            builder.RegisterModule(new DataAccessModule(connectionString, loggerFactory));
            builder.RegisterModule(new DomainModule());
            builder.RegisterModule(new ProcessingModule());
            builder.RegisterModule(new EventsBusModule(eventsBus));
            builder.RegisterModule(new MediatorModule());
            builder.RegisterModule(new OutboxModule());
            builder.RegisterModule(new EmailModule(emailsConfiguration));
            builder.RegisterModule(new QuartzModule());

        

            //TODO: Add Integration Events Events

            builder.RegisterInstance(executionContextAccessor);

            _container = builder.Build();

            UserAccessCompositionRoot.SetContainer(_container);
        }
    }
}
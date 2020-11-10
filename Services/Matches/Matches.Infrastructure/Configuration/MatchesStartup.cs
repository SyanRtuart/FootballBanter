using Autofac;
using Base.Application.BuildingBlocks;
using Base.Application.Emails;
using Base.Infrastructure.Emails;
using Base.Infrastructure.EventBus;
using Matches.Infrastructure.Configuration.DataAccess;
using Matches.Infrastructure.Configuration.Domain;
using Matches.Infrastructure.Configuration.Email;
using Matches.Infrastructure.Configuration.EventsBus;
using Matches.Infrastructure.Configuration.Integration;
using Matches.Infrastructure.Configuration.Logging;
using Matches.Infrastructure.Configuration.Match;
using Matches.Infrastructure.Configuration.Mediator;
using Matches.Infrastructure.Configuration.Processing;
using Matches.Infrastructure.Configuration.Processing.Outbox;
using Matches.Infrastructure.Configuration.Quartz;
using Serilog;
using Serilog.Extensions.Logging;

namespace Matches.Infrastructure.Configuration
{
    public class MatchesStartup
    {
        private static IContainer _container;

        public static void Initialize(string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger,
            EmailsConfiguration emailsConfiguration,
            string textEncryptionKey,
            IEmailSender emailSender,
            IEventsBus eventsBus,
            bool runQuartz = true)
        {
            var moduleLogger = logger.ForContext("Module", "Matches");

            ConfigureCompositionRoot(connectionString, executionContextAccessor, moduleLogger, emailsConfiguration, eventsBus, emailSender);

            if (runQuartz)
            {
                  QuartzStartup.Initialize(moduleLogger);
            }

            EventsBusStartup.Initialize(moduleLogger);
        }

        private static void ConfigureCompositionRoot(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger,
            EmailsConfiguration emailsConfiguration,
            IEventsBus eventsBus,
            IEmailSender emailSender)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new LoggingModule(logger.ForContext("Module", "Matches")));

            var loggerFactory = new SerilogLoggerFactory(logger);
            
         
            builder.RegisterModule(new DataAccessModule(connectionString, loggerFactory));
            builder.RegisterModule(new DomainModule());
            builder.RegisterModule(new ProcessingModule());
            builder.RegisterModule(new EventsBusModule(eventsBus));
            builder.RegisterModule(new MediatorModule());
            builder.RegisterModule(new OutboxModule());
            builder.RegisterModule(new EmailModule(emailsConfiguration, emailSender));
            builder.RegisterModule(new QuartzModule());
            builder.RegisterModule(new IntegrationModule());
            //TODO: Add Integration Events Events

            builder.RegisterInstance(executionContextAccessor);

            _container = builder.Build();

            MatchesCompositionRoot.SetContainer(_container);

        }
    }
}
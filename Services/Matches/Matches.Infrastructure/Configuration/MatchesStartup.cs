using System.Collections.Specialized;
using Autofac;
using Autofac.Extras.Quartz;
using Base.Application.BuildingBlocks;
using Base.Application.Emails;
using Base.Infrastructure.Emails;
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

namespace Matches.Infrastructure.Configuration
{
    public class MatchesStartup
    {
        public static void Initialize(string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger,
            EmailsConfiguration emailsConfiguration,
            string textEncryptionKey,
            IEmailSender emailSender,
            ContainerBuilder builder)
        {
            //var moduleLogger = logger.ForContext("Module", "UserAccess");

            ConfigureCompositionRoot(connectionString,
                executionContextAccessor,
                logger,
                emailsConfiguration,
                textEncryptionKey,
                emailSender,
                builder);

            var schedulerConfiguration = new NameValueCollection
            {
                {"quartz.scheduler.instanceName", "Matches"}
            };

            builder.RegisterModule(new QuartzAutofacFactoryModule
            {
                ConfigurationProvider = c => schedulerConfiguration
            });

            builder.RegisterModule(new QuartzAutofacJobsModule(typeof(ProcessOutboxJob).Assembly));
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
            builder.RegisterModule(new LoggingModule(logger.ForContext("Module", "Matches")));

            var loggerFactory = new Serilog.Extensions.Logging.SerilogLoggerFactory(logger);
            
            builder.RegisterModule(new MatchAutofacModule());
            builder.RegisterModule(new DataAccessModule(connectionString, loggerFactory));
            builder.RegisterModule(new DomainModule());
            builder.RegisterModule(new ProcessingModule());
            builder.RegisterModule(new EventsBusModule());
            builder.RegisterModule(new MediatorModule());
            builder.RegisterModule(new OutboxModule());
            builder.RegisterModule(new EmailModule(emailsConfiguration, emailSender));
            builder.RegisterModule(new QuartzModule());
            builder.RegisterModule(new IntegrationModule());


            builder.RegisterInstance(executionContextAccessor);
        }
    }
}
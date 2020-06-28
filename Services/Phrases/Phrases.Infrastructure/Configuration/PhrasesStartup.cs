using System.Collections.Specialized;
using Autofac;
using Autofac.Extras.Quartz;
using Base.Application.BuildingBlocks;
using Base.Application.Emails;
using Base.Infrastructure.Emails;
using Phrases.Infrastructure.Configuration.DataAccess;
using Phrases.Infrastructure.Configuration.Domain;
using Phrases.Infrastructure.Configuration.Email;
using Phrases.Infrastructure.Configuration.EventsBus;
using Phrases.Infrastructure.Configuration.Logging;
using Phrases.Infrastructure.Configuration.Mediator;
using Phrases.Infrastructure.Configuration.Phrases;
using Phrases.Infrastructure.Configuration.Processing;
using Phrases.Infrastructure.Configuration.Processing.Outbox;
using Phrases.Infrastructure.Configuration.Quartz;
using Serilog;

namespace Phrases.Infrastructure.Configuration
{
    public class PhrasesStartup
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
                {"quartz.scheduler.instanceName", "Phrases"}
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
            builder.RegisterModule(new LoggingModule(logger.ForContext("Module", "Phrases")));

            var loggerFactory = new Serilog.Extensions.Logging.SerilogLoggerFactory(logger);
            
            builder.RegisterModule(new PhrasesAutofacModule());
            builder.RegisterModule(new DataAccessModule(connectionString, loggerFactory));
            builder.RegisterModule(new DomainModule());
            builder.RegisterModule(new ProcessingModule());
            builder.RegisterModule(new EventsBusModule());
            builder.RegisterModule(new MediatorModule());
            builder.RegisterModule(new OutboxModule());
            builder.RegisterModule(new EmailModule(emailsConfiguration, emailSender));
            builder.RegisterModule(new QuartzModule());

            builder.RegisterInstance(executionContextAccessor);
        }
    }
}
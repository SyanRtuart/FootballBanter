using System.Collections.Specialized;
using System.Threading;
using Phrases.Infrastructure.Configuration.Processing.Inbox;
using Phrases.Infrastructure.Configuration.Processing.InternalCommands;
using Phrases.Infrastructure.Configuration.Processing.Outbox;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using Serilog;

namespace Phrases.Infrastructure.Configuration.Quartz
{
    public static class QuartzStartup
    {
        private static IScheduler _scheduler;

        internal static void StopQuartz()
        {
            _scheduler.Shutdown();
        }

        public static void Initialize(ILogger logger)
        {

            logger.Information("Quartz starting...");

            var schedulerConfiguration = new NameValueCollection();
            schedulerConfiguration.Add("quartz.scheduler.instanceName", "Phrases");

            ISchedulerFactory schedulerFactory = new StdSchedulerFactory(schedulerConfiguration);
            _scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();

            LogProvider.SetCurrentLogProvider(new SerilogLogProvider(logger));

            _scheduler.Start().GetAwaiter().GetResult();

            ScheduleProcessOutboxJob(_scheduler);

            ScheduleProcessInboxJob(_scheduler);

            ScheduleProcessInternalCommandsJob(_scheduler);

            logger.Information("Quartz started.");
        }

        private static void ScheduleProcessOutboxJob(IScheduler scheduler)
        {
            var processOutboxJob = JobBuilder.Create<ProcessOutboxJob>().Build();
            var trigger =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithSchedule(SimpleScheduleBuilder.RepeatSecondlyForever(15))
                    .Build();

            scheduler
                .ScheduleJob(processOutboxJob, trigger)
                .GetAwaiter().GetResult();

            scheduler.ScheduleJob(processOutboxJob, trigger).ConfigureAwait(true);
        }

        private static void ScheduleProcessInboxJob(IScheduler scheduler)
        {
            var processInboxJob = JobBuilder.Create<ProcessInboxJob>().Build();
            var processInboxTrigger =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithSchedule(SimpleScheduleBuilder.RepeatSecondlyForever(15))
                    .Build();

            scheduler.ScheduleJob(processInboxJob, processInboxTrigger).ConfigureAwait(true);
        }

        private static void ScheduleProcessInternalCommandsJob(IScheduler scheduler)
        {

            var processInternalCommandsJob = JobBuilder.Create<ProcessInternalCommandsJob>().Build();
            var triggerCommandsProcessing =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithSchedule(SimpleScheduleBuilder.RepeatSecondlyForever(15))
                    .Build();
            scheduler.ScheduleJob(processInternalCommandsJob, triggerCommandsProcessing)
                .ConfigureAwait(true);
        }

    }
}

using System.Collections.Specialized;
using System.Threading;
using Matches.Infrastructure.Configuration.Integration.Matches.SyncMatches;
using Matches.Infrastructure.Configuration.Integration.Teams.SyncTeams;
using Matches.Infrastructure.Configuration.Processing.Inbox;
using Matches.Infrastructure.Configuration.Processing.InternalCommands;
using Matches.Infrastructure.Configuration.Processing.Outbox;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using Serilog;

namespace Matches.Infrastructure.Configuration.Quartz
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
            schedulerConfiguration.Add("quartz.scheduler.instanceName", "Meetings");

            ISchedulerFactory schedulerFactory = new StdSchedulerFactory(schedulerConfiguration);
            _scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();

            LogProvider.SetCurrentLogProvider(new SerilogLogProvider(logger));

            _scheduler.Start().GetAwaiter().GetResult();

            ScheduleProcessOutboxJob(_scheduler);

            ScheduleProcessInboxJob(_scheduler);

            ScheduleProcessInternalCommandsJob(_scheduler);

            ScheduleSyncTeamsJob(_scheduler);

            ScheduleSyncMatchesJob(_scheduler);

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

        private static void ScheduleSyncTeamsJob(IScheduler scheduler)
        {

            var syncTeamsJob = JobBuilder.Create<SyncTeamsJob>().Build();
            var syncTeamsTrigger =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithSchedule(SimpleScheduleBuilder.RepeatHourlyForever(1))
                    .Build();
            scheduler.ScheduleJob(syncTeamsJob, syncTeamsTrigger).ConfigureAwait(true);
        }

        private static void ScheduleSyncMatchesJob(IScheduler scheduler)
        {
            var syncMatchesJob = JobBuilder.Create<SyncMatchesJob>().Build();
            var syncMatchesTrigger =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithSchedule(SimpleScheduleBuilder.RepeatHourlyForever(1))
                    .Build();
            scheduler.ScheduleJob(syncMatchesJob, syncMatchesTrigger).ConfigureAwait(true);
        }


    }
}
using System.Threading;
using Matches.Infrastructure.Configuration.Integration.Matches.SyncMatches;
using Matches.Infrastructure.Configuration.Integration.Teams.SyncTeams;
using Matches.Infrastructure.Configuration.Processing.Inbox;
using Matches.Infrastructure.Configuration.Processing.InternalCommands;
using Matches.Infrastructure.Configuration.Processing.Outbox;
using Quartz;
using Quartz.Logging;
using Serilog;

namespace Matches.Infrastructure.Configuration.Quartz
{
    public static class QuartzStartup
    {
        public static void Initialize(ILogger logger, IScheduler scheduler)
        {
            logger.Information("Quartz starting...");
            var everyFifteenSeconds = "0/15 * * ? * *";
            var fourAmEveryDay = "0 0 4 * * ?";
            var everyHourOnTheHour = "0/1 0 0/1 ? * * *";
            var every60Seconnds = "0/59 * * ? * *";


            LogProvider.SetCurrentLogProvider(new SerilogLogProvider(logger));

            var processOutboxJob = JobBuilder.Create<ProcessOutboxJob>().Build();
            var trigger =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithCronSchedule(everyFifteenSeconds)
                    .Build();

            scheduler
                .ScheduleJob(processOutboxJob, trigger)
                .GetAwaiter().GetResult();
            
            var cts = new CancellationTokenSource();
            scheduler.ScheduleJob(processOutboxJob, trigger, cts.Token).ConfigureAwait(true);

            var processInboxJob = JobBuilder.Create<ProcessInboxJob>().Build();
            var processInboxTrigger =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithCronSchedule(everyFifteenSeconds)
                    .Build();

            scheduler.ScheduleJob(processInboxJob, processInboxTrigger, cts.Token).ConfigureAwait(true);

            var processInternalCommandsJob = JobBuilder.Create<ProcessInternalCommandsJob>().Build(); 
             var triggerCommandsProcessing =
                 TriggerBuilder
                     .Create()
                     .StartNow()
                     .WithCronSchedule(everyFifteenSeconds)
                     .Build();
             scheduler.ScheduleJob(processInternalCommandsJob, triggerCommandsProcessing, cts.Token).ConfigureAwait(true);

            scheduler.Start(cts.Token).ConfigureAwait(true);

            var syncTeamsJob = JobBuilder.Create<SyncTeamsJob>().Build();
            var syncTeamsTrigger =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithCronSchedule(fourAmEveryDay)
                    .Build();
            scheduler.ScheduleJob(syncTeamsJob, syncTeamsTrigger, cts.Token).ConfigureAwait(true);

            var syncMatchesJob = JobBuilder.Create<SyncMatchesJob>().Build();
            var syncMatchesTrigger =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithCronSchedule(fourAmEveryDay)
                    .Build();
            scheduler.ScheduleJob(syncMatchesJob, syncMatchesTrigger, cts.Token).ConfigureAwait(true);
            
            logger.Information("Quartz started.");
        }
    }
}

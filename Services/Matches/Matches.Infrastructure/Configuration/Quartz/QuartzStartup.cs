using System.Threading;
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
           
            LogProvider.SetCurrentLogProvider(new SerilogLogProvider(logger));

            var processOutboxJob = JobBuilder.Create<ProcessOutboxJob>().Build();
            var trigger =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithCronSchedule("0/15 * * ? * *")
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
                    .WithCronSchedule("0/15 * * ? * *")
                    .Build();

            scheduler.ScheduleJob(processInboxJob, processInboxTrigger, cts.Token).ConfigureAwait(true);

            var processInternalCommandsJob = JobBuilder.Create<ProcessInternalCommandsJob>().Build(); 
             var triggerCommandsProcessing =
                 TriggerBuilder
                     .Create()
                     .StartNow()
                     .WithCronSchedule("0/15 * * ? * *")
                     .Build();
             scheduler.ScheduleJob(processInternalCommandsJob, triggerCommandsProcessing, cts.Token).ConfigureAwait(true);

            scheduler.Start(cts.Token).ConfigureAwait(true);
            logger.Information("Quartz started.");
        }
    }
}

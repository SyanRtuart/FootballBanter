using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using Quartz.Spi;
using Serilog;
using UserAccess.Infrastructure.Configuration.Processing.Outbox;

namespace UserAccess.Infrastructure.Configuration.Quartz
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
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(10)
                        .RepeatForever())
                    .Build();

            scheduler
                .ScheduleJob(processOutboxJob, trigger)
                .GetAwaiter().GetResult();

            var cts = new CancellationTokenSource();

            scheduler.ScheduleJob(processOutboxJob, trigger, cts.Token).ConfigureAwait(true);
            //var processInboxJob = JobBuilder.Create<ProcessInboxJob>().Build();
            //var processInboxTrigger =
            //    TriggerBuilder
            //        .Create()
            //        .StartNow()
            //        .WithCronSchedule("0/15 * * ? * *")
            //        .Build();

            //scheduler
            //    .ScheduleJob(processInboxJob, processInboxTrigger)
            //    .GetAwaiter().GetResult();

            //var processInternalCommandsJob = JobBuilder.Create<ProcessInternalCommandsJob>().Build();
            //var triggerCommandsProcessing =
            //    TriggerBuilder
            //        .Create()
            //        .StartNow()
            //        .WithCronSchedule("0/15 * * ? * *")
            //        .Build();
            //scheduler.ScheduleJob(processInternalCommandsJob, triggerCommandsProcessing).GetAwaiter().GetResult();

            //_scheduler = scheduler;
            scheduler.Start(cts.Token).ConfigureAwait(true);
            logger.Information("Quartz started.");
        }
    }
}

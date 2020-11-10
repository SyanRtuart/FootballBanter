using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matches.Infrastructure.Configuration.Processing;
using Matches.Infrastructure.Configuration.Processing.Outbox;
using Quartz;

namespace Matches.Infrastructure.Configuration.Integration.Matches.SyncMatches
{
    [DisallowConcurrentExecution]
    public class SyncMatchesJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await CommandsExecutor.Execute(new SyncMatchesCommand());
        }
    }
}

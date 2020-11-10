using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matches.Infrastructure.Configuration.Processing;
using Quartz;

namespace Matches.Infrastructure.Configuration.Integration.Teams.SyncTeams
{
    [DisallowConcurrentExecution]
    public class SyncTeamsJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await CommandsExecutor.Execute(new SyncTeamsCommand());
        }
    }
}

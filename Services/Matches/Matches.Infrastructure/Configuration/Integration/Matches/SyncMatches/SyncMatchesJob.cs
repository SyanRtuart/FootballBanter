using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matches.Infrastructure.Configuration.Processing;
using Quartz;

namespace Matches.Infrastructure.Configuration.Integration.Matches.SyncMatches
{
    [DisallowConcurrentExecution]
    public class SyncMatchesJob : IJob
    {
        private readonly ICommandExecutor _commandExecutor;

        public SyncMatchesJob(ICommandExecutor commandExecutor)
        {
            _commandExecutor = commandExecutor;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _commandExecutor.Execute(new SyncMatchesCommand());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace UserAccess.Infrastructure.Configuration.Processing.Inbox
{
    [DisallowConcurrentExecution]
    public class ProcessInboxJob : IJob
    {
        private readonly ICommandExecutor _commandExecutor;

        public ProcessInboxJob(ICommandExecutor commandExecutor)
        {
            _commandExecutor = commandExecutor;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _commandExecutor.Execute(new ProcessInboxCommand());
        }
    }
}

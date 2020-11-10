using System.Threading.Tasks;
using Matches.Infrastructure.Configuration.Processing.Outbox;
using Quartz;

namespace Matches.Infrastructure.Configuration.Processing.Inbox
{
    [DisallowConcurrentExecution]
    public class ProcessInboxJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await CommandsExecutor.Execute(new ProcessInboxCommand());
        }
    }
}

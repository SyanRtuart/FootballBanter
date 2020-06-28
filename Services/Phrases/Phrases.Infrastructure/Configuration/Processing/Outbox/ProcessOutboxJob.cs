using System.Threading.Tasks;
using Quartz;

namespace Phrases.Infrastructure.Configuration.Processing.Outbox
{
    [DisallowConcurrentExecution]
    public class ProcessOutboxJob : IJob
    {
        private readonly ICommandExecutor _commandExecutor;
        public ProcessOutboxJob(ICommandExecutor commandExecutor)
        {
            _commandExecutor = commandExecutor;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _commandExecutor.Execute(new ProcessOutboxCommand());
        }
    }
}

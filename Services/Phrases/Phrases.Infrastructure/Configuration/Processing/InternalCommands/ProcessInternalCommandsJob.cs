using System.Threading.Tasks;
using Quartz;

namespace Phrases.Infrastructure.Configuration.Processing.InternalCommands
{
    [DisallowConcurrentExecution]
    public class ProcessInternalCommandsJob : IJob
    {
        private readonly ICommandExecutor _commandExecutor;

        public ProcessInternalCommandsJob(ICommandExecutor commandExecutor)
        {
            _commandExecutor = commandExecutor;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _commandExecutor.Execute(new ProcessInternalCommandsCommand());
        }
    }
}

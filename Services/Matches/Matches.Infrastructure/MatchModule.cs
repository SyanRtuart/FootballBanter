using System.Threading.Tasks;
using Matches.Application.Contracts;
using Matches.Infrastructure.Configuration.Processing;

namespace Matches.Infrastructure
{
    public class MatchModule : IMatchModule
    {
        private readonly ICommandExecutor _commandExecutor;
        private readonly IQueryExecutor _queryExecutor;

        public MatchModule(ICommandExecutor commandExecutor, IQueryExecutor queryExecutor)
        {
            _commandExecutor = commandExecutor;
            _queryExecutor = queryExecutor;
        }

        public async Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command)
        {
            return await _commandExecutor.Execute(command);
        }

        public async Task ExecuteCommandAsync(ICommand command)
        {
            await _commandExecutor.Execute(command);
        }

        public async Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
        {
            return await _queryExecutor.ExecuteQueryAsync(query);
        }
    }
}
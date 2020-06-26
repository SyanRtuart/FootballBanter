using System.Threading.Tasks;
using UserAccess.Application.Contracts;
using UserAccess.Infrastructure.Configuration.Processing;

namespace UserAccess.Infrastructure
{
    public class UserAccessModule : IUserAccessModule
    {
        private readonly ICommandExecutor _commandExecutor;
        private readonly IQueryExecutor _queryExecutor;

        public UserAccessModule(ICommandExecutor commandExecutor, IQueryExecutor queryExecutor)
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
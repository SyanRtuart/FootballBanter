using System.Threading.Tasks;
using Matches.Application.Contracts;

namespace Matches.Infrastructure.Configuration.Processing
{
    public interface ICommandExecutor
    {
        Task Execute(ICommand command);

        Task<TResult> Execute<TResult>(ICommand<TResult> command);
    }
}
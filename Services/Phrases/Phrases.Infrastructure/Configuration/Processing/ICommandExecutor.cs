using System.Threading.Tasks;
using Phrases.Application.Contracts;

namespace Phrases.Infrastructure.Configuration.Processing
{
    public interface ICommandExecutor
    {
        Task Execute(ICommand command);

        Task<TResult> Execute<TResult>(ICommand<TResult> command);
    }
}
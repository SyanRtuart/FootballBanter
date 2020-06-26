using System.Threading.Tasks;
using UserAccess.Application.Contracts;

namespace UserAccess.Infrastructure.Configuration.Processing
{
    public interface ICommandExecutor
    {
        Task Execute(ICommand command);

        Task<TResult> Execute<TResult>(ICommand<TResult> command);
    }
}
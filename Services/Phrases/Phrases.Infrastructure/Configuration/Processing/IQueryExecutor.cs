using System.Threading.Tasks;
using Phrases.Application.Contracts;

namespace Phrases.Infrastructure.Configuration.Processing
{
    public interface IQueryExecutor
    {
        Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);
    }
}
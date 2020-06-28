using System.Threading.Tasks;
using Matches.Application.Contracts;

namespace Matches.Infrastructure.Configuration.Processing
{
    public interface IQueryExecutor
    {
        Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);
    }
}
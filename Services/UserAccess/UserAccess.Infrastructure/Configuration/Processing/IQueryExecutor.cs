using System.Threading.Tasks;
using UserAccess.Application.Contracts;

namespace UserAccess.Infrastructure.Configuration.Processing
{
    public interface IQueryExecutor
    {
        Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);
    }
}
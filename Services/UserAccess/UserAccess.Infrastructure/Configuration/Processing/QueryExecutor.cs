using System.Threading.Tasks;
using MediatR;
using UserAccess.Application.Contracts;

namespace UserAccess.Infrastructure.Configuration.Processing
{
    public class QueryExecutor : IQueryExecutor
    {
        private readonly IMediator _mediator;

        public QueryExecutor(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
        {
            return _mediator.Send(query);
        }
    }
}
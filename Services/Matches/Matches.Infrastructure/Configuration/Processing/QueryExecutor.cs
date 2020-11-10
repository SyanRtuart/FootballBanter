using System.Threading.Tasks;
using Autofac;
using Matches.Application.Contracts;
using MediatR;

namespace Matches.Infrastructure.Configuration.Processing
{
    internal static class QueryExecutor
    {
        public static async Task<TResult> Execute<TResult>(IQuery<TResult> query)
        {
            using (var scope = MatchesCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                return await mediator.Send(query);
            }
        }
    }
}

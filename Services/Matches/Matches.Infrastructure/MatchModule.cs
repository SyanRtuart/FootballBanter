using System.Threading.Tasks;
using Autofac;
using Matches.Application.Contracts;
using Matches.Infrastructure.Configuration;
using Matches.Infrastructure.Configuration.Processing;
using MediatR;

namespace Matches.Infrastructure
{
    public class MatchModule : IMatchModule
    {
        public async Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command)
        {
            return await CommandsExecutor.Execute(command);
        }

        public async Task ExecuteCommandAsync(ICommand command)
        {
            await CommandsExecutor.Execute(command);
        }

        public async Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
        {
            using (var scope = MatchesCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                return await mediator.Send(query);
            }
        }
    }
}
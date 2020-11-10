using System.Threading.Tasks;
using Autofac;
using MediatR;
using Phrases.Application.Contracts;
using Phrases.Infrastructure.Configuration;
using Phrases.Infrastructure.Configuration.Processing;

namespace Phrases.Infrastructure
{
    public class PhrasesModule : IPhrasesModule
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
            using (var scope = PhrasesCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                return await mediator.Send(query);
            }
        }
    }
}
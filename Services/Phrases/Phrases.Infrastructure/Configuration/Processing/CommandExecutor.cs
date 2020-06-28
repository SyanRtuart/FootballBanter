using System.Threading.Tasks;
using MediatR;
using Phrases.Application.Contracts;

namespace Phrases.Infrastructure.Configuration.Processing
{
    public class CommandExecutor : ICommandExecutor
    {
        private readonly IMediator _mediator;

        public CommandExecutor(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Execute(ICommand command)
        {
            await _mediator.Send(command);
        }

        public async Task<TResult> Execute<TResult>(ICommand<TResult> command)
        {
            return await _mediator.Send(command);
        }
    }
}
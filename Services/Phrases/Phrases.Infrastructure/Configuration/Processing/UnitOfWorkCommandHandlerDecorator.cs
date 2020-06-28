using System.Threading;
using System.Threading.Tasks;
using Base.Domain.SeedWork;
using MediatR;
using Phrases.Application.Configuration.Commands;
using Phrases.Application.Contracts;
using Phrases.Infrastructure.Persistence;

namespace Phrases.Infrastructure.Configuration.Processing
{
    internal class UnitOfWorkCommandHandlerDecorator<T> : ICommandHandler<T> where T : ICommand
    {
        private readonly ICommandHandler<T> _decorated;
        private readonly IUnitOfWork _unitOfWork;
        private readonly PhraseContext _phraseContext;

        public UnitOfWorkCommandHandlerDecorator(
            ICommandHandler<T> decorated,
            IUnitOfWork unitOfWork,
            PhraseContext phraseContext)
        {
            _decorated = decorated;
            _unitOfWork = unitOfWork;
            _phraseContext = phraseContext;
        }

        public async Task<Unit> Handle(T command, CancellationToken cancellationToken)
        {
            await _decorated.Handle(command, cancellationToken);

            if (command is InternalCommandBase)
            {
                //var internalCommand = await _phraseContext.InternalCommands.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken: cancellationToken);

                //if (internalCommand != null)
                //{
                //    internalCommand.ProcessedDate = DateTime.UtcNow;
                //}
            }

            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using Base.Domain.SeedWork;
using Phrases.Application.Configuration.Commands;
using Phrases.Application.Contracts;
using Phrases.Infrastructure.Persistence;

namespace Phrases.Infrastructure.Configuration.Processing
{
    internal class UnitOfWorkCommandHandlerWithResultDecorator<T, TResult> : ICommandHandler<T, TResult>
        where T : ICommand<TResult>
    {
        private readonly ICommandHandler<T, TResult> _decorated;
        private readonly IUnitOfWork _unitOfWork;
        private readonly PhraseContext _phraseContext;

        public UnitOfWorkCommandHandlerWithResultDecorator(
            ICommandHandler<T, TResult> decorated,
            IUnitOfWork unitOfWork,
            PhraseContext phraseContext)
        {
            _decorated = decorated;
            _unitOfWork = unitOfWork;
            _phraseContext = phraseContext;
        }

        public async Task<TResult> Handle(T command, CancellationToken cancellationToken)
        {
            var result = await _decorated.Handle(command, cancellationToken);

            if (command is InternalCommandBase<TResult>)
            {
                //var internalCommand = await _phraseContext.InternalCommands.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken: cancellationToken);

                //if (internalCommand != null)
                //{
                //    internalCommand.ProcessedDate = DateTime.UtcNow;
                //}
            }

            await _unitOfWork.CommitAsync(cancellationToken);

            return result;
        }
    }
}
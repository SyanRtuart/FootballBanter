using System.Threading;
using System.Threading.Tasks;
using Base.Domain.SeedWork;
using Matches.Application.Configuration.Commands;
using Matches.Application.Contracts;
using Matches.Infrastructure.Persistence;
using MediatR;

namespace Matches.Infrastructure.Configuration.Processing
{
    internal class UnitOfWorkCommandHandlerDecorator<T> : ICommandHandler<T> where T : ICommand
    {
        private readonly ICommandHandler<T> _decorated;
        private readonly IUnitOfWork _unitOfWork;
        private readonly MatchContext _matchContext;

        public UnitOfWorkCommandHandlerDecorator(
            ICommandHandler<T> decorated,
            IUnitOfWork unitOfWork,
            MatchContext matchContext)
        {
            _decorated = decorated;
            _unitOfWork = unitOfWork;
            _matchContext = matchContext;
        }

        public async Task<Unit> Handle(T command, CancellationToken cancellationToken)
        {
            await _decorated.Handle(command, cancellationToken);

            if (command is InternalCommandBase)
            {
                //var internalCommand = await _matchContext.InternalCommands.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken: cancellationToken);

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
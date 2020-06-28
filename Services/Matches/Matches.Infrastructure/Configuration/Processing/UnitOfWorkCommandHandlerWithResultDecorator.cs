using System.Threading;
using System.Threading.Tasks;
using Base.Domain.SeedWork;
using Matches.Application.Configuration.Commands;
using Matches.Application.Contracts;
using Matches.Infrastructure.Persistence;

namespace Matches.Infrastructure.Configuration.Processing
{
    internal class UnitOfWorkCommandHandlerWithResultDecorator<T, TResult> : ICommandHandler<T, TResult>
        where T : ICommand<TResult>
    {
        private readonly ICommandHandler<T, TResult> _decorated;
        private readonly IUnitOfWork _unitOfWork;
        private readonly MatchContext _matchContext;

        public UnitOfWorkCommandHandlerWithResultDecorator(
            ICommandHandler<T, TResult> decorated,
            IUnitOfWork unitOfWork,
            MatchContext matchContext)
        {
            _decorated = decorated;
            _unitOfWork = unitOfWork;
            _matchContext = matchContext;
        }

        public async Task<TResult> Handle(T command, CancellationToken cancellationToken)
        {
            var result = await _decorated.Handle(command, cancellationToken);

            if (command is InternalCommandBase<TResult>)
            {
                //var internalCommand = await _matchContext.InternalCommands.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken: cancellationToken);

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
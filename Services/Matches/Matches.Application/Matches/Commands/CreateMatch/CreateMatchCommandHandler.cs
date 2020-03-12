using System.Threading;
using System.Threading.Tasks;
using Matches.Domain.Aggregates.Match;
using MediatR;

namespace Matches.Application.Matches.Commands.CreateMatch
{
    public class CreateMatchCommandHandler : IRequestHandler<CreateMatchCommand, bool>
    {
        private readonly IMatchRepository _matchRepository;

        public CreateMatchCommandHandler(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public async Task<bool> Handle(CreateMatchCommand request, CancellationToken cancellationToken)
        {
            var match = Match.Create(request.HomeTeamId,
                request.AwayTeamId,
                request.UtcDate,
                new Score(request.ScoreWinner, request.ScoreHomeTeam, request.ScoreAwayTeam));

            await _matchRepository.AddAsync(match);

            return await _matchRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
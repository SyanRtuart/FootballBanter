using System;
using System.Threading;
using System.Threading.Tasks;
using Matches.Domain.Match;
using MediatR;

namespace Matches.Application.Matches.Commands.CreateMatch
{
    public class CreateMatchCommandHandler : IRequestHandler<CreateMatchCommand, Guid>
    {
        private readonly IMatchRepository _matchRepository;

        public CreateMatchCommandHandler(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public async Task<Guid> Handle(CreateMatchCommand request, CancellationToken cancellationToken)
        {
            var match = Match.Create(request.HomeTeamId,
                request.AwayTeamId,
                request.UtcDate,
                new Score(request.ScoreWinner, request.ScoreHomeTeam, request.ScoreAwayTeam));

            await _matchRepository.AddAsync(match);

            await _matchRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return match.Id;
        }
    }
}
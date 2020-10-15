using System;
using System.Threading;
using System.Threading.Tasks;
using Matches.Application.Configuration.Commands;
using Matches.Domain.Match;
using Matches.Domain.Team;

namespace Matches.Application.Matches.Commands.CreateMatch
{
    public class CreateMatchCommandHandler : ICommandHandler<CreateMatchCommand, Guid>
    {
        private readonly IMatchRepository _matchRepository;

        public CreateMatchCommandHandler(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public async Task<Guid> Handle(CreateMatchCommand request, CancellationToken cancellationToken)
        {
            var match = Match.CreateNew(request.Name, new TeamId(request.HomeTeamId), new TeamId(request.AwayTeamId), 
                request.Score, request.Season,
                request.UtcDate, request.ExternalId, request.Status);

            await _matchRepository.AddAsync(match);

            return match.Id.Value;
        }
    }
}
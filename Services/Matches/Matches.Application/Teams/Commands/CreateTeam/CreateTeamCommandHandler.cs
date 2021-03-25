using System;
using System.Threading;
using System.Threading.Tasks;
using Matches.Application.Configuration.Commands;
using Matches.Domain.Team;

namespace Matches.Application.Teams.Commands.CreateTeam
{
    public class CreateTeamCommandHandler : ICommandHandler<CreateTeamCommand, Guid>
    {
        private readonly ITeamRepository _teamRepository;

        public CreateTeamCommandHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<Guid> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            var team = Team.CreateNew(request.Name, request.Description, request.Logo, request.Manager, request.League,
                request.Country, request.FormedYear, request.Facebook, request.Instagram, request.Stadium, request.ExternalId);

            await _teamRepository.AddAsync(team);

            return team.Id.Value;
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using Matches.Domain.Team;
using MediatR;

namespace Matches.Application.Teams.Commands.CreateTeam
{
    public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, Guid>
    {
        private readonly ITeamRepository _teamRepository;

        public CreateTeamCommandHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<Guid> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            var team = Team.Create(request.Name);

            await _teamRepository.AddAsync(team);

            return team.Id;
        }
    }
}
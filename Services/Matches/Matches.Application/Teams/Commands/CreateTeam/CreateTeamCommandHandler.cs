using System.Threading;
using System.Threading.Tasks;
using Matches.Domain.Team;
using MediatR;

namespace Matches.Application.Teams.Commands.CreateTeam
{
    public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, bool>
    {
        private readonly ITeamRepository _teamRepository;

        public CreateTeamCommandHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<bool> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            var team = Team.Create(request.Name);

            await _teamRepository.AddAsync(team);

            return await _teamRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
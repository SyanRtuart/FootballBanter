using System.Threading;
using System.Threading.Tasks;
using Matches.Application.Configuration.Commands;
using Matches.Domain.Team;
using MediatR;

namespace Matches.Application.Teams.Commands.EditTeamGeneralAttributes
{
    public class EditTeamGeneralAttributesCommandHandler : ICommandHandler<EditTeamGeneralAttributesCommand>
    {
        private readonly ITeamRepository _teamRepository;

        public EditTeamGeneralAttributesCommandHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }


        public async Task<Unit> Handle(EditTeamGeneralAttributesCommand request, CancellationToken cancellationToken)
        {
            var team = await _teamRepository.GetAsync(new TeamId(request.TeamId));

            team.EditGeneralAttributes(request.Name, request.Description, request.Logo, request.Manager, request.League,
                request.Country, request.FormedYear, request.Facebook, request.Instagram, request.Stadium, request.ExternalId);

            await _teamRepository.CommitAsync();

            return Unit.Value;
        }
    }
}
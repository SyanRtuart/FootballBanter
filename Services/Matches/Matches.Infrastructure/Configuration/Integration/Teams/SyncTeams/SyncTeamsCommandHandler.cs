using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Matches.Application.Configuration.Commands;
using Matches.Application.Contracts;
using Matches.Application.Teams.Commands.CreateTeam;
using Matches.Application.Teams.Commands.EditTeamGeneralAttributes;
using Matches.Application.Teams.Queries.GetAllTeams;
using Matches.Domain.Team;
using Matches.Infrastructure.Configuration.Processing;
using MediatR;

namespace Matches.Infrastructure.Configuration.Integration.Teams.SyncTeams
{
    public class SyncTeamsCommandHandler : ICommandHandler<SyncTeamsCommand>
    {
        private readonly IIntegrationService _integrationService;
        private readonly HttpClient _httpClient;
        private readonly IMatchModule _matchModule;

        public SyncTeamsCommandHandler(IIntegrationService integrationService, HttpClient httpClient, IMatchModule matchModule)
        {
            _integrationService = integrationService;
            _httpClient = httpClient;
            _matchModule = matchModule;
        }

        public async Task<Unit> Handle(SyncTeamsCommand request, CancellationToken cancellationToken)
        {
            var teamsInDb = await _matchModule.ExecuteQueryAsync(new GetAllTeamsQuery());

            var response = await _integrationService.GetTeams("Scotland");

            foreach (var teamResponse in response.Teams)
            {
                byte[] logo = null;
                if (!string.IsNullOrEmpty(teamResponse.strTeamBadge))
                {
                    logo = await _httpClient.GetByteArrayAsync(teamResponse.strTeamBadge + "/preview");
                }

                if (!TeamExistsInDatabase(out var id, teamsInDb, teamResponse.strTeam))
                {
                    await AddTeam(teamResponse, logo);
                }
                else
                {
                    await EditTeam(id, teamResponse, logo);
                }
            }

            return Unit.Value;
        }

        private bool TeamExistsInDatabase(out Guid id,  List<TeamDto> teamsInDb , string nameIn)
        {
            foreach (var team in teamsInDb)
            {
                var name = team.Name;
                if (Equals(name, nameIn))
                {
                    id = team.Id;
                    return true;
                }
            }

            id = Guid.Empty;
            return false;
        }

        private async Task AddTeam(TeamResponse team, byte[] logo)
        {
            var command = new CreateTeamCommand(team.strTeam, team.strDescriptionEN, logo,
                team.strManager, team.strLeague, team.strCountry, int.Parse(team.intFormedYear),
                team.strFacebook,
                team.strInstagram,
                Stadium.CreateNew(team.strStadium, team.strStadiumDescription, team.strStadiumLocation), 
                team.idTeam);

           await _matchModule.ExecuteCommandAsync(command);
        }

        private async Task EditTeam(Guid id, TeamResponse team, byte[] logo)
        {
            var command = new EditTeamGeneralAttributesCommand(id, team.strTeam, team.strDescriptionEN, logo,
                team.strManager, team.strLeague, team.strCountry, int.Parse(team.intFormedYear),
                team.strFacebook,
                team.strInstagram,
                Stadium.CreateNew(team.strStadium, team.strStadiumDescription, team.strStadiumLocation), 
                team.idTeam);

            await _matchModule.ExecuteCommandAsync(command);
        }
    }
    
}
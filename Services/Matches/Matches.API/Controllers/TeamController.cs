using System.Collections.Generic;
using System.Threading.Tasks;
using Base.Api.Configuration.Authorization;
using Matches.Application.Contracts;
using Matches.Application.Teams.Commands.CreateTeam;
using Matches.Application.Teams.Queries.GetAllTeams;
using Matches.Infrastructure.Configuration.Integration;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Matches.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly IMatchModule _matchModule;

        public TeamController(IMatchModule matchModule)
        {
            _matchModule = matchModule;
        }

        [HttpGet]
        public async Task<List<TeamDto>> GetAll()
        {
            return await _matchModule.ExecuteQueryAsync(new GetAllTeamsQuery());
        }
    }
}
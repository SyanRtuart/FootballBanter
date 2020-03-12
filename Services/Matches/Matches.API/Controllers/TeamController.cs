using System.Collections.Generic;
using System.Threading.Tasks;
using Matches.Application.Teams.Commands.CreateTeam;
using Matches.Application.Teams.Queries.GetAllTeams;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Matches.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TeamController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<TeamDto>> GetAll()
        {
            return await _mediator.Send(new GetAllTeamsQuery());
        }

        [HttpPost]
        public async Task Post(string name)
        {
            await _mediator.Send(new CreateTeamCommand(name));
        }
    }
}

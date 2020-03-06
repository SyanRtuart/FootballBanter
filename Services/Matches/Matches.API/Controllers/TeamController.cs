using System.Collections.Generic;
using System.Threading.Tasks;
using Matches.Application.Teams.Commands.AddTeam;
using Matches.Application.Teams.Queries.GetAllTeams;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Teams.Domain.Aggregates;
using Teams.Domain.Aggregates.TeamAggregate;

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
        public async Task<TeamsViewModel> GetAll()
        {
            return await _mediator.Send(new GetAllTeamsCommand());
        }

        [HttpPost]
        public async Task Post(string name)
        {
            await _mediator.Send(new AddTeamCommand(name));
        }
    }
}

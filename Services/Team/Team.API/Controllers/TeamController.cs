using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Teams.Application.Teams.Commands.AddTeam;
using Teams.Application.Teams.Queries.GetAllTeams;
using Teams.Domain.Aggregates;

namespace Teams.API.Controllers
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
        public async Task<List<Team>> GetAll()
        {
          return   await _mediator.Send(new GetAllTeamsCommand());
        }

        [HttpPost]
        public async Task Post(string name)
        {
            await _mediator.Send(new AddTeamCommand(name));
        }
    }
}

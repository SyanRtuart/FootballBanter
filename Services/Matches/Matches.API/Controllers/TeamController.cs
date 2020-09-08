using System.Collections.Generic;
using System.Threading.Tasks;
using Base.Api.Configuration.Authorization;
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
    [Authorize]
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
    }
}
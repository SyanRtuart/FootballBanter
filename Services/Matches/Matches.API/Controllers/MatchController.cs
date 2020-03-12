using System.Collections.Generic;
using System.Threading.Tasks;
using Matches.Application.Matches.Commands.CreateMatch;
using Matches.Application.Matches.Queries.GetRecentMatchesByTeam;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Matches.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MatchController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MatchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMatch([FromBody] CreateMatchRequest request)
        {
            await _mediator.Send(new CreateMatchCommand(request.HomeTeamId, request.AwayTeamId, request.UtcDate,
                request.Status, request.ScoreWinner, request.ScoreHomeTeam, request.ScoreAwayTeam));

            return Ok();
        }

        [HttpGet]
        public async Task<List<MatchDto>> GetRecentMatchesByTeam(int teamId)
        {
            return await _mediator.Send(new GetRecentMatchesByTeamQuery(teamId));
        }
    }
}
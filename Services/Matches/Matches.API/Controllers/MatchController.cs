using System.Threading.Tasks;
using Matches.Application.Matches.Commands.CreateMatch;
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
    }
}

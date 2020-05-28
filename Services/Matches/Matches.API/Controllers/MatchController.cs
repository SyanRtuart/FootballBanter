using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Matches.Application.Matches.Commands.CreateMatch;
using Matches.Application.Matches.Queries.GetMatchById;
using Matches.Application.Matches.Queries.GetRecentMatchesByTeam;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MatchDto = Matches.Application.Matches.SharedModels.MatchDto;

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

        [HttpGet]
        [Route("{id}")]
        public async Task<MatchDto> GetMatchById(Guid id)
        {
            return await _mediator.Send(new GetMatchByIdQuery(id));
        }
        
        [HttpGet]
        public async Task<List<MatchDto>> GetRecentMatchesByTeam([FromQuery] Guid teamId)
        {
            return await _mediator.Send(new GetRecentMatchesByTeamQuery(teamId));
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
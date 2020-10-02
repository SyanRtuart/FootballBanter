using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Matches.Application.Contracts;
using Matches.Application.Matches.Commands.CreateMatch;
using Matches.Application.Matches.Queries.GetMatchById;
using Matches.Application.Matches.Queries.GetMatchesByTeam;
using Matches.Application.Matches.SharedModels;
using Matches.Domain.Match;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Matches.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MatchController : ControllerBase
    {
        private readonly IMatchModule _matchModule;

        public MatchController(IMatchModule matchModule)
        {
            _matchModule = matchModule;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<MatchDto> GetMatchById(Guid id)
        {
            return await _matchModule.ExecuteQueryAsync(new GetMatchByIdQuery(id));
        }

        [HttpGet]
        public async Task<List<MatchDto>> GetRecentMatchesByTeam([FromQuery] Guid teamId)
        {
            return await _matchModule.ExecuteQueryAsync(new GetMatchesByTeamQuery(teamId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateMatch([FromBody] CreateMatchRequest request)
        {
            await _matchModule.ExecuteCommandAsync(new CreateMatchCommand(request.Name, request.HomeTeamId, request.AwayTeamId,
                request.UtcDate, request.Status, 
                Score.CreateNew(request.ScoreWinner, request.ScoreHomeTeam, request.ScoreAwayTeam), 
                request.Season, request.ExternalId));

            return Ok();
        }
    }
}
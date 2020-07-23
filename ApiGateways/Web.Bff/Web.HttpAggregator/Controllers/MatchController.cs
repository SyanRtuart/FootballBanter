using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.HttpAggregator.Models.Match;
using Web.HttpAggregator.Services.Match;

namespace Web.HttpAggregator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MatchController : ControllerBase
    {
        private readonly IMatchApiClient _matchApiClient;

        public MatchController(IMatchApiClient matchApiClient)
        {
            _matchApiClient = matchApiClient;
        }

        [HttpGet]
        [Route("getTeams")]
        public async Task<List<Team>> GetTeamsAsync()
        {
            var response = await _matchApiClient.GetTeamsAsync();

            var teams = new List<Team>();

            foreach (var teamData in response)
            {
                teams.Add(new Team(teamData.Id, teamData.Name, teamData.Logo, teamData.League, teamData.Country));
            }

            return teams;
        }

        [HttpGet]
        [Route("getRecentMatches")]
        public async Task<List<Match>> GetRecentMatchesAsync(Guid teamId)
        {
            var response = await _matchApiClient.GetRecentMatchesAsync(teamId);

            var matches = new List<Match>();

            foreach (var matchData in response)
            {
                matches.Add(new Match(matchData.Id, matchData.HomeTeamId, matchData.AwayTeamId, matchData.HomeTeam,
                    matchData.AwayTeam, matchData.ScoreWinner, matchData.ScoreHomeTeam, matchData.ScoreAwayTeam));
            }

            return matches;
        }

        [HttpGet]
        [Route("getMatch")]
        public async Task<Match> GetMatchById(Guid id)
        {
            var response = await _matchApiClient.GetMatch(id);

            var match = new Match(response.Id, response.HomeTeamId, response.AwayTeamId, response.HomeTeam,
                response.AwayTeam, response.ScoreWinner, response.ScoreHomeTeam, response.ScoreAwayTeam);

            return match;
        }
    }
}
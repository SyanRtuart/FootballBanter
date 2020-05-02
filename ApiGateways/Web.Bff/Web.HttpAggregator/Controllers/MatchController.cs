using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.HttpAggregator.Models.Match;
using Web.HttpAggregator.Services.Match;

namespace Web.HttpAggregator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MatchController
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
                teams.Add(new Team(teamData.Id, teamData.Name));
            }

            return teams;
        }

        [HttpGet]
        [Route("getRecentMatches")]
        public async Task<List<Match>> GetRecentMatchesAsync(int teamId)
        {
            var response = await _matchApiClient.GetRecentMatchesAsync(teamId);

            var matches = new List<Match>();

            foreach (var matchData in response)
                matches.Add(new Match(matchData.Id, matchData.HomeTeamId, matchData.AwayTeamId, matchData.HomeTeam,
                    matchData.AwayTeam, matchData.Winner, matchData.HomeTeamScore, matchData.AwayTeamScore));

            return matches;
        }
    }
}
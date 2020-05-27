using System.Collections.Generic;
using System.Threading.Tasks;
using Web.HttpAggregator.Models.Match;

namespace Web.HttpAggregator.Services.Match
{
    public interface IMatchApiClient
    {
        Task<List<TeamData>> GetTeamsAsync();

        Task<List<MatchData>> GetRecentMatchesAsync(int teamId);

        Task<MatchData> GetMatch(int id);

    }
}

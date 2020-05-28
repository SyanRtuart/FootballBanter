using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.HttpAggregator.Models.Match;

namespace Web.HttpAggregator.Services.Match
{
    public interface IMatchApiClient
    {
        Task<List<TeamData>> GetTeamsAsync();

        Task<List<MatchData>> GetRecentMatchesAsync(Guid teamId);

        Task<MatchData> GetMatch(Guid id);

    }
}

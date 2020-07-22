using System.Collections.Generic;
using System.Threading.Tasks;
using Matches.Infrastructure.Configuration.Integration.Matches;
using Matches.Infrastructure.Configuration.Integration.Teams;

namespace Matches.Infrastructure.Configuration.Integration
{
    public interface IIntegrationService
    {
        Task<TeamsResponse> GetTeams(string country);

        Task<MatchesResponse> GetNext15Matches(string leagueId);

        Task<MatchesResponse> GetLast15Matches(string leagueId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matches.Infrastructure.Configuration.Integration
{
    public class UrlsConfig
    {
        private string _apiKey = "1";

        public UrlsConfig()
        {
            
        }

        public string GetAllTeams(string country) => $"https://www.thesportsdb.com/api/v1/json/{_apiKey}/search_all_teams.php?s=Soccer&c={country}";

        public string GetNext15Matches(string leagueId) => $"https://www.thesportsdb.com/api/v1/json/{_apiKey}/eventsnextleague.php?id={leagueId}";

        public string GetLast15Matches(string leagueId) => $"https://www.thesportsdb.com/api/v1/json/{_apiKey}/eventspastleague.php?id={leagueId}";
    }
}

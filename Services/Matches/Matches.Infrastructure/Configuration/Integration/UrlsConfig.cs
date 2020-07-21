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

        public string GetAllTeams()
        {
            return
                $"https://www.thesportsdb.com/api/v1/json/{_apiKey}/search_all_teams.php?l=scottish%20Premier%20League";
        }
    }
}

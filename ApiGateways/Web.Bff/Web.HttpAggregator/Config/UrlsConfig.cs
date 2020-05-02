using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;

namespace Web.HttpAggregator.Config
{
    public class UrlsConfig
    {
        public string Match { get; set; }
        public string Phrase { get; set; }

        public class MatchOperations
        {
            public static string GetTeams() => "team";

            public static string GetRecentMatches(int teamId) => QueryHelpers.AddQueryString("match", nameof(teamId), teamId.ToString());

        }

        public class PhraseOperations
        {
            public static string GetPhrases(int matchId)
            {
                var queryArguments = new Dictionary<string, string>
                {
                    {nameof(matchId), matchId.ToString()},
                };

                return QueryHelpers.AddQueryString("phrase", queryArguments);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

            public static string GetRecentMatches(Guid teamId) => QueryHelpers.AddQueryString("match", nameof(teamId), teamId.ToString());

            public static string GetMatch(Guid id) => $"match/{id}";

        }

        public class PhraseOperations
        {
            public static string GetPhrases(Guid matchId)
            {
                var queryArguments = new Dictionary<string, string>
                {
                    {nameof(matchId), matchId.ToString()},
                };

                return QueryHelpers.AddQueryString("phrase", queryArguments);
            }

            public static string CreatePhrase => "phrase";
            public static string UpvotePhrase(Guid phraseId) => $"phrase/{phraseId}/upvote";
            public static string DownvotePhrase(Guid phraseId) => $"phrase/{phraseId}/downvote";


        }
    }
}

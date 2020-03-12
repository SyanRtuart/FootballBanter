using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.HttpAggregator.Models.Match
{
    public class Match
    {
        public Match(int id, int homeTeamId, int awayTeamId, string homeTeam, string awayTeam, string winner, int homeTeamScore, int awayTeamScore)
        {
            Id = id;
            HomeTeamId = homeTeamId;
            AwayTeamId = awayTeamId;
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            Winner = winner;
            HomeTeamScore = homeTeamScore;
            AwayTeamScore = awayTeamScore;
        }

        public int Id { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string Winner { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.HttpAggregator.Models.Match
{
    public class Match
    {
        public Match(int id, int homeTeamId, int awayTeamId, string homeTeam, string awayTeam, string scoreWinner, int scoreHomeTeam, int scoreAwayTeam)
        {
            Id = id;
            HomeTeamId = homeTeamId;
            AwayTeamId = awayTeamId;
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            ScoreWinner = scoreWinner;
            ScoreHomeTeam = scoreHomeTeam;
            ScoreAwayTeam = scoreAwayTeam;
        }

        public int Id { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string ScoreWinner { get; set; }
        public int ScoreHomeTeam { get; set; }
        public int ScoreAwayTeam { get; set; }
    }
}

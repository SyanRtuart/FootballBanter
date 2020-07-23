using System;

namespace Web.HttpAggregator.Models.Match
{
    public class Match
    {
        public Match(Guid id, string name, Guid homeTeamId, Guid awayTeamId, string homeTeam, string awayTeam, DateTime utcDate, string scoreWinner, int scoreHomeTeam, int scoreAwayTeam, string season, string status, byte[] homeTeamLogo, byte[] awayTeamLogo)
        {
            Id = id;
            Name = name;
            HomeTeamId = homeTeamId;
            AwayTeamId = awayTeamId;
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            UtcDate = utcDate;
            ScoreWinner = scoreWinner;
            ScoreHomeTeam = scoreHomeTeam;
            ScoreAwayTeam = scoreAwayTeam;
            Season = season;
            Status = status;
            HomeTeamLogo = homeTeamLogo;
            AwayTeamLogo = awayTeamLogo;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid HomeTeamId { get; set; }
        public Guid AwayTeamId { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public DateTime UtcDate { get; set; }
        public string ScoreWinner { get; set; }
        public int ScoreHomeTeam { get; set; }
        public int ScoreAwayTeam { get; set; }
        public string Season { get; set; }
        public string Status { get; set; }
        public byte[] HomeTeamLogo { get; set; }
        public byte[] AwayTeamLogo { get; set; }
    }
}
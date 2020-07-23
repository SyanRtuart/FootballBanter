using System;

namespace Web.HttpAggregator.Models.Match
{
    public class MatchData
    {
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
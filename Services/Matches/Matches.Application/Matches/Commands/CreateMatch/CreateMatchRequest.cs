using System;

namespace Matches.Application.Matches.Commands.CreateMatch
{
    public class CreateMatchRequest
    {
        public string Name { get; set; }
        public Guid HomeTeamId { get; set; }
        public Guid AwayTeamId { get; set; }
        public DateTime UtcDate { get; set; }
        public string Status { get; set; }
        public string ScoreWinner { get; set; }
        public int ScoreHomeTeam { get; set; }
        public int ScoreAwayTeam { get; set; }
        public string Season { get; set; }
        public string ExternalId { get; set; }
    }
}
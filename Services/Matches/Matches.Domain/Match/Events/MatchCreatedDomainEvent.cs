using System;
using Base.Domain;

namespace Matches.Domain.Match.Events
{
    public class MatchCreatedDomainEvent : DomainEventBase
    {
        public MatchCreatedDomainEvent(Guid matchId, Guid homeTeamId, Guid awayTeamId, string externalId,
            string name, Score score, string season, string status, DateTime utcDate)
        {
            MatchId = matchId;
            HomeTeamId = homeTeamId;
            AwayTeamId = awayTeamId;
            ExternalId = externalId;
            Name = name;
            Score = score;
            Season = season;
            Status = status;
            UtcDate = utcDate;
        }

        public Guid MatchId { get; set; }

        public Guid HomeTeamId { get; set; }

        public Guid AwayTeamId { get; set; }

        public string ExternalId { get; set; }

        public string Name { get; set; }

        public Score Score { get; set; }

        public string Season { get; set; }

        public string Status { get; set; }

        public DateTime UtcDate { get; set; }
    }
}

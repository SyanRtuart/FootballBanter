using System;
using Base.Domain;
using Matches.Domain.Team;

namespace Matches.Domain.Match.Events
{
    public class MatchCreatedDomainEvent : DomainEventBase
    {
        public MatchCreatedDomainEvent(MatchId matchId, TeamId homeTeamId, TeamId awayTeamId, string externalId,
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

        public MatchId MatchId { get; set; }

        public TeamId HomeTeamId { get; set; }

        public TeamId AwayTeamId { get; set; }

        public string ExternalId { get; set; }

        public string Name { get; set; }

        public Score Score { get; set; }

        public string Season { get; set; }

        public string Status { get; set; }

        public DateTime UtcDate { get; set; }
    }
}

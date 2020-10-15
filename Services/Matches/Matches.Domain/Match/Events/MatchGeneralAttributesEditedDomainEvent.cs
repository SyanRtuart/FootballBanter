using System;
using Base.Domain;

namespace Matches.Domain.Match.Events
{
    public class MatchGeneralAttributesEditedDomainEvent : DomainEventBase
    {
        public MatchGeneralAttributesEditedDomainEvent(MatchId matchId, string newName, DateTime newUtcDate, Score newScore, string newSeason, string newStatus)
        {
            MatchId = matchId;
            NewName = newName;
            NewUtcDate = newUtcDate;
            NewScore = newScore;
            NewSeason = newSeason;
            NewStatus = newStatus;
        }

        public MatchId MatchId { get; set; }
        public string NewName { get; set; }
        public DateTime NewUtcDate { get; set; }
        public Score NewScore { get; set; }
        public string NewSeason { get; set; }
        public string NewStatus { get; set; }
    }
}

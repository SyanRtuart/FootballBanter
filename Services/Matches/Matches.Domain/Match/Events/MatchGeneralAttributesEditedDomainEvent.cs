using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Domain;

namespace Matches.Domain.Match.Events
{
    public class MatchGeneralAttributesEditedDomainEvent : DomainEventBase
    {
        public MatchGeneralAttributesEditedDomainEvent(string newName, DateTime newUtcDate, Score newScore, string newSeason, string newStatus)
        {
            NewName = newName;
            NewUtcDate = newUtcDate;
            NewScore = newScore;
            NewSeason = newSeason;
            NewStatus = newStatus;
        }

        public string NewName { get; set; }
        public DateTime NewUtcDate { get; set; }
        public Score NewScore { get; set; }
        public string NewSeason { get; set; }
        public string NewStatus { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Domain;

namespace Matches.Domain.Match.Events
{
    public class MatchCreatedDomainEvent : DomainEventBase
    {
        public MatchCreatedDomainEvent(Guid matchId)
        {
            MatchId = matchId;
        }

        public Guid MatchId { get; set; }
    }
}

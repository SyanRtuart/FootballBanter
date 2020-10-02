using System;
using Base.Domain;

namespace Matches.Domain.Team.Events
{
    public class TeamCreatedDomainEvent : DomainEventBase
    {
        public TeamCreatedDomainEvent(Guid teamId)
        {
            TeamId = teamId;
        }

        public Guid TeamId { get; set; }
    }
}
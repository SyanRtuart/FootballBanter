using System;
using Base.Domain;

namespace Matches.Domain.Team.Events
{
    public class TeamCreatedDomainEvent : DomainEventBase
    {
        public TeamCreatedDomainEvent(TeamId teamId)
        {
            TeamId = teamId;
        }

        public TeamId TeamId { get; set; }
    }
}
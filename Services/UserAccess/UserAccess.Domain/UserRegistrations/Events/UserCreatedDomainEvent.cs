using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Domain;

namespace UserAccess.Domain.UserRegistrations.Events
{
    public class UserCreatedDomainEvent : DomainEventBase
    {
        public UserCreatedDomainEvent(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Domain;

namespace UserAccess.Domain.UserRegistrations.Events
{
    public class UserRegistrationExpiredDomainEvent : DomainEventBase
    {
        public UserRegistrationExpiredDomainEvent(Guid userRegistrationId)
        {
            UserRegistrationId = userRegistrationId;
        }

        public Guid UserRegistrationId { get; set; }
    }
}

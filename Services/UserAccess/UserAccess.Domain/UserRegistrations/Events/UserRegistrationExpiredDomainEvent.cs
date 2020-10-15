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
        public UserRegistrationExpiredDomainEvent(UserRegistrationId userRegistrationId)
        {
            UserRegistrationId = userRegistrationId;
        }

        public UserRegistrationId UserRegistrationId { get; set; }
    }
}

using System;
using Base.Domain;
using UserAccess.Domain.Users;

namespace UserAccess.Domain.UserRegistrations.Events
{
    public class UserRegistrationConfirmedDomainEvent : DomainEventBase
    {
        public UserRegistrationConfirmedDomainEvent(UserRegistrationId userRegistrationId)
        {
            UserRegistrationId = userRegistrationId;
        }

        public UserRegistrationId UserRegistrationId { get; }
    }
}
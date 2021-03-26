using System;
using Base.Domain;

namespace UserAccess.Domain.Users.Events
{
    public class UserChangedPasswordDomainEvent : DomainEventBase
    {
        public UserChangedPasswordDomainEvent(UserId userId)
        {
            UserId = userId;
        }

        public UserId UserId { get; set; }
    }
}

using Base.Domain;
using UserAccess.Domain.Users;

namespace UserAccess.Domain.UserRegistrations.Events
{
    public class UserCreatedDomainEvent : DomainEventBase
    {
        public UserCreatedDomainEvent(UserId userId)
        {
            UserId = userId;
        }

        public UserId UserId { get; set; }
    }
}

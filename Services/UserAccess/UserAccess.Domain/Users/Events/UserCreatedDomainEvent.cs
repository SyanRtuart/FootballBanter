using Base.Domain;

namespace UserAccess.Domain.Users.Events
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

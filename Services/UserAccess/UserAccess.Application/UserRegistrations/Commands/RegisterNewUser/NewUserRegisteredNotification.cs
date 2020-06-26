using Base.Application.Events;
using Newtonsoft.Json;
using UserAccess.Domain.UserRegistrations.Events;

namespace UserAccess.Application.UserRegistrations.Commands.RegisterNewUser
{
    public class NewUserRegisteredNotification : DomainNotificationBase<NewUserRegisteredDomainEvent>
    {
        [JsonConstructor]
        public NewUserRegisteredNotification(NewUserRegisteredDomainEvent domainEvent) : base(domainEvent)
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Application.Events;
using Newtonsoft.Json;
using UserAccess.Domain.UserRegistrations.Events;

namespace UserAccess.Application.Users.Commands.CreateUser
{
    public class UserRegistrationConfirmedNotification : DomainNotificationBase<UserRegistrationConfirmedDomainEvent>
    {
        [JsonConstructor]
        public UserRegistrationConfirmedNotification(UserRegistrationConfirmedDomainEvent domainEvent) : base(domainEvent)
        {
        }
    }
}

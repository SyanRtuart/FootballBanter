using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Domain;

namespace UserAccess.Domain.Users.Events
{
    public class UserGeneralAttributesEditedDomainEvent : DomainEventBase
    {
        public UserGeneralAttributesEditedDomainEvent(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

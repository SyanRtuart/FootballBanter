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
        public UserGeneralAttributesEditedDomainEvent(string firstName, string lastName, byte[] picture)
        {
            FirstName = firstName;
            LastName = lastName;
            Picture = picture;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Picture { get; set; }
    }
}

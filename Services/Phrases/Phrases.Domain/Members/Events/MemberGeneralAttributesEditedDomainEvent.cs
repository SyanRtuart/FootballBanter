using System;
using Base.Domain;

namespace Phrases.Domain.Members.Events
{
    public class MemberGeneralAttributesEditedDomainEvent : DomainEventBase
    {
        public MemberGeneralAttributesEditedDomainEvent(Guid memberId, string firstName, string lastName, byte[] picture)
        {
            MemberId = memberId;
            FirstName = firstName;
            LastName = lastName;
            Picture = picture;
        }

        public Guid MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Picture { get; set; }
    }
}

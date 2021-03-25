using Base.Domain;

namespace Phrases.Domain.Members.Events
{
    public class MemberGeneralAttributesEditedDomainEvent : DomainEventBase
    {
        public MemberGeneralAttributesEditedDomainEvent(string firstName, string lastName, byte[] picture)
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

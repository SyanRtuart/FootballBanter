using System;
using Base.Infrastructure.EventBus;

namespace Phrases.IntegrationEvents
{
    public class MemberGeneralAttributesEditedIntegrationEvent : IntegrationEvent
    {
        public Guid MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Picture { get; set; }

        public MemberGeneralAttributesEditedIntegrationEvent(Guid id, DateTime occurredOn, Guid memberId, string firstName, string lastName, byte[] picture) : base(id, occurredOn)
        {
            MemberId = memberId;
            FirstName = firstName;
            LastName = lastName;
            Picture = picture;
        }
    }
}

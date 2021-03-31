using Base.Application.Events;
using Newtonsoft.Json;
using Phrases.Domain.Members.Events;

namespace Phrases.Application.Members.Commands.EditMemberGeneralAttributes
{
    public class MemberGeneralAttributesEditedNotification : DomainNotificationBase<MemberGeneralAttributesEditedDomainEvent>
    {
        [JsonConstructor]
        public MemberGeneralAttributesEditedNotification(MemberGeneralAttributesEditedDomainEvent domainEvent) : base(domainEvent)
        {
        }
    }
}

using Base.Application.Events;
using Newtonsoft.Json;
using Phrases.Domain.Phrase.Events;

namespace Phrases.Application.Phrases.Commands.CreatePhrase
{
    public class PhraseCreatedNotification : DomainNotificationBase<PhraseCreatedDomainEvent>
    {
        [JsonConstructor]
        public PhraseCreatedNotification(PhraseCreatedDomainEvent domainEvent) : base(domainEvent)
        {
        }
    }
}

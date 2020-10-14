using System;
using Base.Domain;

namespace Phrases.Domain.Phrase.Events
{
    public class PhraseDeletedDomainEvent : DomainEventBase
    {
        public PhraseDeletedDomainEvent(Guid phraseId, Guid deletedByUserId, DateTime utcDateDeleted)
        {
            PhraseId = phraseId;
            DeletedByUserId = deletedByUserId;
            UtcDateDeleted = utcDateDeleted;
        }

        public Guid PhraseId { get; set; }

        public Guid DeletedByUserId { get; set; }

        public DateTime UtcDateDeleted { get; set; }
    }
}

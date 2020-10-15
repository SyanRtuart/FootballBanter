using System;
using Base.Domain;
using Phrases.Domain.User;

namespace Phrases.Domain.Phrase.Events
{
    public class PhraseDeletedDomainEvent : DomainEventBase
    {
        public PhraseDeletedDomainEvent(PhraseId phraseId, UserId deletedByUserId, DateTime utcDateDeleted)
        {
            PhraseId = phraseId;
            DeletedByUserId = deletedByUserId;
            UtcDateDeleted = utcDateDeleted;
        }

        public PhraseId PhraseId { get; set; }

        public UserId DeletedByUserId { get; set; }

        public DateTime UtcDateDeleted { get; set; }
    }
}

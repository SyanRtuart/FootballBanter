using System;
using Base.Domain;

namespace Phrases.Domain.Phrase.Events
{
    public class PhraseVoteHistoryCreatedDomainEvent : DomainEventBase
    {
        public PhraseVoteHistoryCreatedDomainEvent(Guid phraseVoteHistoryId, Guid phraseId, Guid userId, int score)
        {
            PhraseVoteHistoryId = phraseVoteHistoryId;
            PhraseId = phraseId;
            UserId = userId;
            Score = score;
        }

        public Guid PhraseVoteHistoryId { get; set; }
        public Guid PhraseId { get; set; }
        public Guid UserId { get; set; }
        public int Score { get; set; }
    }
}

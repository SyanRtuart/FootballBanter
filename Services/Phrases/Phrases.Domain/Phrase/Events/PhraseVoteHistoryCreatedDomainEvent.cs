using System;
using Base.Domain;
using Phrases.Domain.User;

namespace Phrases.Domain.Phrase.Events
{
    public class PhraseVoteHistoryCreatedDomainEvent : DomainEventBase
    {
        public PhraseVoteHistoryCreatedDomainEvent(PhraseVoteHistoryId phraseVoteHistoryId, PhraseId phraseId, UserId userId, int score)
        {
            PhraseVoteHistoryId = phraseVoteHistoryId;
            PhraseId = phraseId;
            UserId = userId;
            Score = score;
        }

        public PhraseVoteHistoryId PhraseVoteHistoryId { get; set; }
        public PhraseId PhraseId { get; set; }
        public UserId UserId { get; set; }
        public int Score { get; set; }
    }
}

using System;
using Base.Domain.SeedWork;
using Phrases.Domain.Phrase.Events;

namespace Phrases.Domain.Phrase
{
    public class PhraseVoteHistory : Entity, IAggregateRoot
    {
        private PhraseVoteHistory(Guid phraseId, Guid userId, int score)
        {
            Id = Guid.NewGuid();
            PhraseId = phraseId;
            UserId = userId;
            _score = score;

            AddDomainEvent(new PhraseVoteHistoryCreatedDomainEvent(Id, phraseId, userId, score));
        }

        public PhraseVoteHistory()
        {
            // Only for ORM
        }

        internal Guid PhraseId { get; }

        internal Guid UserId { get; }

        private int _score { get; }

        private DateTime _utcDateVoted { get; }

        private DateTime _utcDateDeleted { get; }

        internal static PhraseVoteHistory CreateNew(Guid phraseId, Guid userId, int score)
        {
            return new PhraseVoteHistory(phraseId, userId, score);
        }

        internal bool HasAlreadyUpvoted(Guid userId)
        {
            return UserId == userId && _score == 1;
        }

        internal bool HasAlreadyDownvoted(Guid userId)
        {
            return UserId == userId && _score == -1;
        }
    }
}

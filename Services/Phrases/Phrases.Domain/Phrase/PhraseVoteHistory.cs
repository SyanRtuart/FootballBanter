using System;
using Base.Domain.SeedWork;
using Phrases.Domain.Phrase.Events;
using Phrases.Domain.User;

namespace Phrases.Domain.Phrase
{
    public class PhraseVoteHistory : Entity
    {
        public PhraseVoteHistoryId Id { get;}

        private PhraseVoteHistory(PhraseId phraseId, UserId userId, int score)
        {
            Id = new PhraseVoteHistoryId(Guid.NewGuid());
            PhraseId = phraseId;
            UserId = userId;
            _utcDateVoted = DateTime.UtcNow;
            _score = score;

            AddDomainEvent(new PhraseVoteHistoryCreatedDomainEvent(Id, phraseId, userId, score));
        }

        public PhraseVoteHistory()
        {
            // Only for ORM
        }

        internal PhraseId PhraseId { get; }

        internal UserId UserId { get; }

        private int _score { get; }

        private DateTime _utcDateVoted { get; }

        private DateTime _utcDateDeleted { get; }

        internal static PhraseVoteHistory CreateNew(PhraseId phraseId, UserId userId, int score)
        {
            return new PhraseVoteHistory(phraseId, userId, score);
        }

        internal bool HasAlreadyUpvoted(UserId userId)
        {
            return UserId == userId && _score == 1;
        }

        internal bool HasAlreadyDownvoted(UserId userId)
        {
            return UserId == userId && _score == -1;
        }
    }
}

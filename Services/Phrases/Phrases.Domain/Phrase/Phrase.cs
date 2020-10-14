using System;
using System.Collections.Generic;
using Base.Domain.SeedWork;
using Phrases.Domain.Phrase.Events;
using Phrases.Domain.Phrase.Rules;

namespace Phrases.Domain.Phrase
{
    public class Phrase : Entity, IAggregateRoot
    {
        private DateTime _dateCreated;

        private DateTime _dateDeleted;

        private string _description;

        private Guid _matchId;

        private bool _positive;

        private int _score;

        private Guid _teamId;

        private List<PhraseVoteHistory> _phraseVoteHistory;

        private Guid _createdByUserId;

        private Guid _deletedByUserId;

        private Phrase(Guid matchId, Guid teamId, Guid createdByUserId, string description, bool positive)
        {
            Id = Guid.NewGuid();
            _matchId = matchId;
            _teamId = teamId;
            _createdByUserId = createdByUserId;
            _description = description;
            _positive = positive;
            _dateCreated = DateTime.UtcNow;
            _score = 0;
            _phraseVoteHistory = new List<PhraseVoteHistory>();

            AddDomainEvent(new PhraseCreatedDomainEvent(Id, matchId, teamId, createdByUserId, description, positive, _dateCreated));
        }

        public static Phrase Create(Guid matchId, Guid teamId, Guid createdByUserId, string description, bool positive)
        {
            var phrase = new Phrase(matchId, teamId, createdByUserId, description, positive);

            phrase.Upvote(createdByUserId);
            
            return phrase;
        }

        public void Delete(Guid deletedByUserId)
        {
            if (_deletedByUserId == Guid.Empty)
            {
                _dateDeleted = DateTime.UtcNow;
                _deletedByUserId = deletedByUserId;
            }

            AddDomainEvent(new PhraseDeletedDomainEvent(Id, deletedByUserId, _dateDeleted));
        }

        public void Upvote(Guid userId)
        {
            CheckRule(new UserCannotUpvoteTwiceRule(userId, _phraseVoteHistory));

            _score += 1;

            _phraseVoteHistory.Add(PhraseVoteHistory.CreateNew(Id, userId, 1));
        }

        public void Downvote(Guid userId)
        {
            CheckRule(new UserCannotDownvoteTwiceRule(userId, _phraseVoteHistory));

            _score += -1;

            _phraseVoteHistory.Add(PhraseVoteHistory.CreateNew(Id, userId, -1));
        }
    }
}
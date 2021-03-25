using System;
using System.Collections.Generic;
using Base.Domain.SeedWork;
using Phrases.Domain.Match;
using Phrases.Domain.Phrase.Events;
using Phrases.Domain.Phrase.Rules;
using Phrases.Domain.Team;
using Phrases.Domain.User;

namespace Phrases.Domain.Phrase
{
    public class Phrase : Entity, IAggregateRoot
    {
        public PhraseId Id { get; }

        private DateTime _dateCreated;

        private DateTime? _dateDeleted;

        private string _description;

        private MatchId _matchId;

        private bool _positive;

        private int _score;

        private TeamId _teamId;

        private List<PhraseVoteHistory> _phraseVoteHistory;

        private UserId _createdByUserId;

        private UserId? _deletedByUserId;

        private Phrase(MatchId matchId, TeamId teamId, UserId createdByUserId, string description, bool positive)
        {
            Id = new PhraseId(Guid.NewGuid());
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

        public static Phrase CreateNew(MatchId matchId, TeamId teamId, UserId createdByUserId, string description, bool positive)
        {
            var phrase = new Phrase(matchId, teamId, createdByUserId, description, positive);

            phrase.Upvote(createdByUserId);
            
            return phrase;
        }

        public void Delete(UserId deletedByUserId)
        {
            if (_deletedByUserId == null)
            {
                _dateDeleted = DateTime.UtcNow;
                _deletedByUserId = deletedByUserId;

                AddDomainEvent(new PhraseDeletedDomainEvent(Id, deletedByUserId, _dateDeleted.Value));
            }

        }

        public void Upvote(UserId userId)
        {
            CheckRule(new UserCannotUpvoteTwiceRule(userId, _phraseVoteHistory));

            _score += 1;

            _phraseVoteHistory.Add(PhraseVoteHistory.CreateNew(Id, userId, 1));
        }

        public void Downvote(UserId userId)
        {
            CheckRule(new UserCannotDownvoteTwiceRule(userId, _phraseVoteHistory));

            _score += -1;

            _phraseVoteHistory.Add(PhraseVoteHistory.CreateNew(Id, userId, -1));
        }
    }
}
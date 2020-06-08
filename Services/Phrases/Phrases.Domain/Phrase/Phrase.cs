using System;
using Base.Domain.SeedWork;

namespace Phrases.Domain.Phrase
{
    public class Phrase : Entity, IAggregateRoot
    {
        private DateTime _dateCreated;

        private DateTime _dateDeleted;

        private string _description;

        private bool _isDeleted;

        private Guid _matchId;

        private bool _positive;

        private int _score;

        private Guid _teamId;

        private Phrase(Guid matchId, Guid teamId, string description, bool positive)
        {
            _matchId = matchId;
            _teamId = teamId;
            _description = description;
            _positive = positive;
            _dateCreated = DateTime.UtcNow;
            _score = 0;
        }

        public static Phrase Create(Guid matchId, Guid teamId, string description, bool positive)
        {
            var phrase = new Phrase(matchId, teamId, description, positive);

            //TODO The user creating should upvote
            phrase.Upvote();

            return phrase;
            //TODO EVENT: Phrase Created Event
        }

        public void Delete()
        {
            if (!_isDeleted)
            {
                _isDeleted = true;
                _dateDeleted = DateTime.UtcNow;
            }

            //TODO EVENT: Phrase Deleted Event
        }

        public void Upvote()
        {
            _score += 1;
        }

        public void Downvote()
        {
            _score += -1;
        }
    }
}
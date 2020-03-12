using System;
using Base.Domain.SeedWork;

namespace Phrases.Domain.Aggregates.PhraseAggregate
{
    public class Phrase : Entity, IAggregateRoot
    {
        private DateTime _dateCreated;

        private string _description;

        private int _matchId;

        private bool _positive;

        private int _teamId;

        public Phrase(int matchId, int teamId, string description, bool positive)
        {
            _matchId = matchId;
            _teamId = teamId;
            _description = description;
            _positive = positive;
            _dateCreated = DateTime.UtcNow;
        }

        public static Phrase Create(int matchId, int teamId, string description, bool positive)
        {
            return new Phrase(matchId, teamId, description, positive);
        }
    }
}
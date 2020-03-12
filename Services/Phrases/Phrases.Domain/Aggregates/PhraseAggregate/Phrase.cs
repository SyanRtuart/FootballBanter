using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Domain.SeedWork;

namespace Phrases.Domain.Aggregates.PhraseAggregate
{
    public class Phrase : Entity, IAggregateRoot
    {
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

        private int _matchId;

        private int _teamId;

        private string _description;

        private bool _positive;

        private DateTime _dateCreated;

    }
}

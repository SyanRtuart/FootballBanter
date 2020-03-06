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
        public Phrase(string description, int matchId, int teamId, bool positive)
        {
            Description = description;
            MatchId = matchId;
            TeamId = teamId;
            Positive = positive;
        }

        public string Description { get; private set; }

        public int MatchId { get; private set; }

        public int TeamId { get; private set; }

        public bool Positive { get; private set; }
    }
}

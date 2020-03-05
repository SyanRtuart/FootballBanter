using System.Collections.Generic;
using Base.Domain.SeedWork;

namespace Teams.Domain.Aggregates
{
    public class Team : Entity, IAggregateRoot
    {
        public string Name { get; private set; }

        private readonly List<Player> _players;
        public IReadOnlyCollection<Player> Players => _players;

        protected Team()
        {
            _players = new List<Player>();
        }

        public Team(string name) : this()
        {
            Name = name;
        }

    }
}

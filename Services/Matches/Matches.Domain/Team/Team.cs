using System;
using Base.Domain.SeedWork;

namespace Matches.Domain.Team
{
    public class Team : Entity, IAggregateRoot
    {
        private string _name;

        private Team(string name)
        {
            Id = Guid.NewGuid();
            _name = name;
        }

        public Team()
        {
            // Only for ORM.
        }

        public static Team Create(string name)
        {
            return new Team(name);
        }
    }
}
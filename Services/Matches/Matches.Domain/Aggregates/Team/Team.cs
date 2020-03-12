using Base.Domain.SeedWork;

namespace Matches.Domain.Aggregates.Team
{
    public class Team : Entity, IAggregateRoot
    {
        private string _name;

        private Team(string name)
        {
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
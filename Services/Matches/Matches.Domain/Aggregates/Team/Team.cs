using Base.Domain.SeedWork;

namespace Matches.Domain.Aggregates.Team
{
    public class Team  : Entity, IAggregateRoot
    {
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

        private string _name;
    }
}

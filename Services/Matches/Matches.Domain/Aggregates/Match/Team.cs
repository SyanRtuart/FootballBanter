using Base.Domain.SeedWork;

namespace Matches.Domain.Aggregates.Match
{
    public class Team  : Entity, IAggregateRoot
    {
        public Team(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}

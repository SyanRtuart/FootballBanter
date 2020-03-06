using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Domain.SeedWork;

namespace Teams.Domain.Aggregates.MatchAggregate
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

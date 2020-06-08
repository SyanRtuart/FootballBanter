using System;

namespace Web.HttpAggregator.Models.Match
{
    public class Team
    {
        public Team(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
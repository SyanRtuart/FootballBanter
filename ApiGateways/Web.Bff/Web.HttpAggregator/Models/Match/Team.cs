using System;

namespace Web.HttpAggregator.Models.Match
{
    public class Team
    {
        public Team(Guid id, string name, byte[] logo, string league, string country)
        {
            Id = id;
            Name = name;
            Logo = logo;
            League = league;
            Country = country;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] Logo { get; set; }
        public string League { get; set; }
        public string Country { get; set; }
    }
}
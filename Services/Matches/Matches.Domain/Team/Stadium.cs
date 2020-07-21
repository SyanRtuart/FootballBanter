using Base.Domain.SeedWork;

namespace Matches.Domain.Team
{
    public class Stadium : ValueObject
    {
        public Stadium(string name, string description, string location)
        {
            Name = name;
            Description = description;
            Location = location;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }
    }
}
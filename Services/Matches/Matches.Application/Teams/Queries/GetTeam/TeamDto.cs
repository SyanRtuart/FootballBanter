using System;

namespace Matches.Application.Teams.Queries.GetTeam
{
    public class TeamDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Logo { get; set; }
        public string Manager { get; set; }
        public string League { get; set; }
        public string Country { get; set; }
        public int FormedYear { get; set; }
        public string StadiumName { get; set; }
        public string StadiumLocation { get; set; }
        public string StadiumDescription { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string ExternalId { get; set; }
    }
}

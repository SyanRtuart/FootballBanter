using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matches.Application.Contracts;
using Matches.Domain.Team;

namespace Matches.Application.Teams.Commands.EditTeamGeneralAttributes
{
    public class EditTeamGeneralAttributesCommand : CommandBase
    {
        public EditTeamGeneralAttributesCommand(Guid teamId, string name, string description, byte[] logo, string manager, string league, 
            string country, int formedYear, string facebook, string instagram, Stadium stadium, string externalId)
        {
            TeamId = teamId;
            Name = name;
            Description = description;
            Logo = logo;
            Manager = manager;
            League = league;
            Country = country;
            FormedYear = formedYear;
            Facebook = facebook;
            Instagram = instagram;
            Stadium = stadium;
            ExternalId = externalId;
        }

        public Guid TeamId { get; set; }
        public string Name { get; }
        public string Description { get; set; }
        public byte[] Logo { get; set; }
        public string Manager { get; set; }
        public string League { get; set; }
        public string Country { get; set; }
        public int FormedYear { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public Stadium Stadium { get; set; }
        public string ExternalId { get; set; }
    }
}

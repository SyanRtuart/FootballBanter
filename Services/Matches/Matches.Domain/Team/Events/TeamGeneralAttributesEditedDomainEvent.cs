using Base.Domain;

namespace Matches.Domain.Team.Events
{
    public class TeamGeneralAttributesEditedDomainEvent : DomainEventBase
    {
        public TeamGeneralAttributesEditedDomainEvent(string newName, string newDescription, byte[] newLogo,
            string newManager, string newLeague, string newCountry, int newFormedYear, string newFacebook,
            string newInstagram, Stadium newStadium, string newExternalId)
        {
            NewName = newName;
            NewDescription = newDescription;
            NewLogo = newLogo;
            NewManager = newManager;
            NewLeague = newLeague;
            NewCountry = newCountry;
            NewFormedYear = newFormedYear;
            NewFacebook = newFacebook;
            NewInstagram = newInstagram;
            NewStadium = newStadium;
            NewExternalId = newExternalId;
        }

        public string NewName { get; set; }
        public string NewDescription { get; set; }
        public byte[] NewLogo { get; set; }
        public string NewManager { get; set; }
        public string NewLeague { get; set; }
        public string NewCountry { get; set; }
        public int NewFormedYear { get; set; }
        public string NewFacebook { get; set; }
        public string NewInstagram { get; set; }
        public Stadium NewStadium { get; set; }
        public string NewExternalId { get; set; }
    }
}
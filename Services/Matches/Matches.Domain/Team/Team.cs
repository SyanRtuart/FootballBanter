using Base.Domain.SeedWork;

namespace Matches.Domain.Team
{
    public class Team : Entity, IAggregateRoot
    {
        private string _name;

        private string _description;

        private byte[] _logo;

        private string _manager;

        private string _league;

        private string _country;

        private int _formedYear;

        private string _facebook;

        private string _instagram;

        private Stadium _stadium;

        private string _externalId;

        public Team(string name, string description, byte[] logo, string manager, string league, string country, 
            int formedYear, string facebook, string instagram, Stadium stadium, string externalId)
        {
            _name = name;
            _description = description;
            _logo = logo;
            _manager = manager;
            _league = league;
            _country = country;
            _formedYear = formedYear;
            _facebook = facebook;
            _instagram = instagram;
            _stadium = stadium;
            _externalId = externalId;
        }

        public Team()
        {
            // Only for ORM.
        }

        public void EditGeneralAttributes(string name, string description, byte[] logo, string manager, string league,
            string country, int formedYear, string facebook, string instagram, Stadium stadium, string externalId)
        {
            _name = name;
            _description = description;
            _logo = logo;
            _manager = manager;
            _league = league;
            _country = country;
            _formedYear = formedYear;
            _facebook = facebook;
            _instagram = instagram;
            _stadium = stadium;
            _externalId = externalId;

            //TODO: Add Domain event
        }

        public static Team Create(string name, string description, byte[] logo, string manager, string league, 
            string country, int formedYear, string facebook, string instagram, Stadium stadium, string externalId)
        {
            return new Team(name, description, logo, manager, league, country, formedYear, facebook, instagram, stadium, externalId);

            //TODO: Add Domain event
        }

        public string Name()
        {
            return _name;
        }

    }
}
using System;
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

        public Team(string name, string description, byte[] logo, string manager, string league, string country, 
            int formedYear, string facebook, string instagram, Stadium stadium)
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
        }

        public Team()
        {
            // Only for ORM.
        }

        public void EditGeneralAttributes(string name, string description, byte[] logo, string manager, string league,
            string country, int formedYear, string facebook, string instagram, Stadium stadium)
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

            //TODO: Add Domain event
        }

        public static Team Create(string name, string description, byte[] logo, string manager, string league, 
            string country, int formedYear, string facebook, string instagram, Stadium stadium)
        {
            return new Team(name, description, logo, manager, league, country, formedYear, facebook, instagram, stadium);

            //TODO: Add Domain event
        }

        public string Name()
        {
            return _name;
        }
    }
}
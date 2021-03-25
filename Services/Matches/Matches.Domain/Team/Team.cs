using System;
using Base.Domain.SeedWork;
using Matches.Domain.Team.Events;

namespace Matches.Domain.Team
{
    public class Team : Entity, IAggregateRoot
    {
        public TeamId Id { get; }

        private string _country;

        private string _description;

        private string _externalId;

        private string _facebook;

        private int _formedYear;

        private string _instagram;

        private string _league;

        private byte[] _logo;

        private string _manager;

        private string _name;

        private Stadium _stadium;

        private Team(string name, string description, byte[] logo, string manager, string league, string country,
            int formedYear, string facebook, string instagram, Stadium stadium, string externalId)
        {
            Id = new TeamId(Guid.NewGuid());
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

            AddDomainEvent(new TeamCreatedDomainEvent(Id));
        }

        private Team()
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

            AddDomainEvent(new TeamGeneralAttributesEditedDomainEvent(name, description, logo, manager, league, country,
                formedYear, facebook, instagram, stadium, externalId));
        }

        public static Team CreateNew(string name, string description, byte[] logo, string manager, string league,
            string country, int formedYear, string facebook, string instagram, Stadium stadium, string externalId)
        {
            return new Team(name, description, logo, manager, league, country, formedYear, facebook, instagram, stadium,
                externalId);
        }
    }
}
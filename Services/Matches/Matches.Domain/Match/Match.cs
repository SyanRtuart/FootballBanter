﻿using System;
using Base.Domain.SeedWork;
using Matches.Domain.Match.Events;

namespace Matches.Domain.Match
{
    public class Match : Entity, IAggregateRoot
    {
        private Guid _awayTeamId;

        private string _externalId;

        private Guid _homeTeamId;

        private string _name;

        private Score _score;

        private string _season;

        private string _status;

        private DateTime _utcDate;

        public Match(string name, Guid homeTeamId, Guid awayTeamId, Score score, string season, DateTime utcDate,
            string externalId, string status)
        {
            Id = Guid.NewGuid();
            _name = name;
            _awayTeamId = awayTeamId;
            _homeTeamId = homeTeamId;
            _score = score;
            _season = season;
            _utcDate = utcDate;
            _externalId = externalId;
            _status = status;

            AddDomainEvent(new MatchCreatedDomainEvent(Id));
        }

        private Match()
        {
            // Only for ORM.
        }

        public static Match CreateNew(string name, Guid homeTeamId, Guid awayTeamId, Score score, string season,
            DateTime utcDate, string externalId, string status)
        {
            return new Match(name, homeTeamId, awayTeamId, score, season, utcDate, externalId, status);
        }

        public void EditGeneralAttributes(string name, DateTime utcDate, Score score, string season, string status)
        {
            _name = name;
            _utcDate = utcDate;
            _score = score;
            _season = season;
            _status = status;

            AddDomainEvent(new MatchGeneralAttributesEditedDomainEvent(name, utcDate, score, season, status));
        }
    }
}
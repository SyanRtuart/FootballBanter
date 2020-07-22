using System;
using Base.Domain.SeedWork;

namespace Matches.Domain.Match
{
    public class Match : Entity, IAggregateRoot
    {
        private string _name;

        private Guid _homeTeamId;

        private Guid _awayTeamId;

        private Score _score;

        private string _season;

        private DateTime _utcDate;

        private string _externalId;

        private string _status;

        public Match(string name, Guid homeTeamId, Guid awayTeamId, Score score, string season, DateTime utcDate, string externalId, string status)
        {
            _name = name;
            _awayTeamId = awayTeamId;
            _homeTeamId = homeTeamId;
            _score = score;
            _season = season;
            _utcDate = utcDate;
            _externalId = externalId;
            _status = status;

            //TODO: Add Domain event

        }

        private Match()
        {
            // Only for ORM.
        }

        public static Match Create(string name, Guid homeTeamId, Guid awayTeamId, Score score, string season, DateTime utcDate, string externalId, string status)
        {
            return new Match(name, awayTeamId, homeTeamId, score, season, utcDate, externalId, status);

        }

        public void EditGeneralAttributes(string name, DateTime utcDate, Score score, string season, string status)
        {
            _name = name;
            _utcDate = utcDate;
            _score = score;
            _season = season;
            _status = status;

            //TODO: Add Domain event
        }
    }
}
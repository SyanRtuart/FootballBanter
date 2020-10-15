using System;
using Base.Domain.SeedWork;
using Matches.Domain.Match.Events;
using Matches.Domain.Team;

namespace Matches.Domain.Match
{
    public class Match : Entity, IAggregateRoot
    {
        public MatchId Id { get; set; }

        private readonly TeamId _awayTeamId;

        private readonly string _externalId;

        private readonly TeamId _homeTeamId;

        private string _name;

        private Score _score;

        private string _season;

        private string _status;

        private DateTime _utcDate;

        public Match(string name, TeamId homeTeamId, TeamId awayTeamId, Score score, string season, DateTime utcDate,
            string externalId, string status)
        {
            Id = new MatchId(Guid.NewGuid());
            _name = name;
            _awayTeamId = awayTeamId;
            _homeTeamId = homeTeamId;
            _score = score;
            _season = season;
            _utcDate = utcDate;
            _externalId = externalId;
            _status = status;

            AddDomainEvent(new MatchCreatedDomainEvent(Id, _homeTeamId, _awayTeamId, _externalId, _name, _score, _season, _status, _utcDate));
        }

        private Match()
        {
            // Only for ORM.
        }

        public static Match CreateNew(string name, TeamId homeTeamId, TeamId awayTeamId, Score score, string season,
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

            AddDomainEvent(new MatchGeneralAttributesEditedDomainEvent(Id, name, utcDate, score, season, status));
        }
    }
}
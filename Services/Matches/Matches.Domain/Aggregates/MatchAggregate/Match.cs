using System;
using Base.Domain.SeedWork;

namespace Matches.Domain.Aggregates.MatchAggregate
{
    public class Match : Entity, IAggregateRoot
    {
        public Match(int homeTeamId, int awayTeamId, DateTime utcDate, MatchStatus status, Score score)
        {

        }

        private Match()
        {
            // Only for ORM.
        }

        public static Match Create(int homeTeamId, int awayTeamId, DateTime utcDate, MatchStatus status, Score score)
        {
            return new Match(homeTeamId, awayTeamId, utcDate, status, score);
        }

        public MatchStatus Status { get; private set; }
        private int _statusId;

        private int _homeTeamId;
        private int _awayTeamId;

        private DateTime _utcDate;

        private MatchStatus _status;

        private Score _score;
    }
}

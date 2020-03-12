using System;
using Base.Domain.SeedWork;

namespace Matches.Domain.Aggregates.Match
{
    public class Match : Entity, IAggregateRoot
    {
        private int _awayTeamId;

        private int _homeTeamId;

        private Score _score;


        private int _statusId;

        private DateTime _utcDate;

        private Match(int homeTeamId, int awayTeamId, DateTime utcDate, Score score)
        {
        }

        private Match()
        {
            // Only for ORM.
        }

        public static Match Create(int homeTeamId, int awayTeamId, DateTime utcDate, Score score)
        {
            return new Match(homeTeamId, awayTeamId, utcDate, score);
        }
    }
}
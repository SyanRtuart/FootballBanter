using System;
using Base.Domain.SeedWork;

namespace Matches.Domain.Aggregates.Match
{
    public class Match : Entity, IAggregateRoot
    {
        private Match(int homeTeamId, int awayTeamId, DateTime utcDate, Score score)
        {

        }

        private Match()
        {
            // Only for ORM.
        }

        public static Match Create(int homeTeamId, int awayTeamId, DateTime utcDate, Score score)
        {
            return new Match(homeTeamId, awayTeamId, utcDate,  score);
        }


        private int _statusId;

        private int _homeTeamId;
        private int _awayTeamId;

        private DateTime _utcDate;

        private Score _score;
    }
}

using System;
using Base.Domain.SeedWork;

namespace Matches.Domain.Match
{
    public class Match : Entity, IAggregateRoot
    {
        private Match(int homeTeamId, int awayTeamId, DateTime utcDate, Score score)
        {
            _homeTeamId = homeTeamId;
            _awayTeamId = awayTeamId;
            _utcDate = utcDate;
            _score = score;
        }

        private int _homeTeamId;

        private int _awayTeamId;

        private DateTime _utcDate;

        private Score _score;


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
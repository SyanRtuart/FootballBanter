using System;
using Base.Domain.SeedWork;

namespace Matches.Domain.Match
{
    public class Match : Entity, IAggregateRoot
    {
        private Guid _awayTeamId;

        private Guid _homeTeamId;

        private Score _score;

        private DateTime _utcDate;

        private Match(Guid homeTeamId, Guid awayTeamId, DateTime utcDate, Score score)
        {
            _homeTeamId = homeTeamId;
            _awayTeamId = awayTeamId;
            _utcDate = utcDate;
            _score = score;
        }


        private Match()
        {
            // Only for ORM.
        }

        public static Match Create(Guid homeTeamId, Guid awayTeamId, DateTime utcDate, Score score)
        {
            return new Match(homeTeamId, awayTeamId, utcDate, score);
        }
    }
}
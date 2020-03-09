using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Domain.SeedWork;

namespace Teams.Domain.Aggregates.MatchAggregate
{
    public class Match : Entity, IAggregateRoot
    {
        public Match(Team homeTeam, Team awayTeam, DateTime utcDate, MatchStatus status, Score score)
        {
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            UtcDate = utcDate;
            Status = status;
            Score = score;
        }

        public Match()
        {
            
        }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public DateTime UtcDate { get; set; }
        public MatchStatus Status { get; set; }
        public Score Score { get; set; }
    }
}

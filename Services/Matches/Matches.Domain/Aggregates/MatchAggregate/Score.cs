using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Domain.SeedWork;

namespace Teams.Domain.Aggregates.MatchAggregate
{
    public class Score : ValueObject
    {
        public Score(string winner, int homeTeam, int awayTeam)
        {
            Winner = winner;
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
        }

        public Score() { }

        public string Winner { get; private set; }

        public int HomeTeam { get; private set; }
        public int AwayTeam { get; private set; }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Winner;
            yield return HomeTeam;
            yield return AwayTeam;
        }
    }
}

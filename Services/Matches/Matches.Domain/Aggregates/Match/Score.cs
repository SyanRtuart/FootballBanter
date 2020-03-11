using System.Collections.Generic;
using Base.Domain.SeedWork;

namespace Matches.Domain.Aggregates.Match
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
    }
}

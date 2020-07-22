using Base.Domain.SeedWork;

namespace Matches.Domain.Match
{
    public class Score : ValueObject
    {
        public Score(string winner, int? homeTeam, int? awayTeam)
        {
            Winner = winner;
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
        }

        public Score()
        {
        }

        public string Winner { get; }
        public int? HomeTeam { get; }
        public int? AwayTeam { get; }
    }
}
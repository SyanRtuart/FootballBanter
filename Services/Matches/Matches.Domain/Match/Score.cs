using Base.Domain.SeedWork;

namespace Matches.Domain.Match
{
    public class Score : ValueObject
    {
        public static Score CreateNew(string winner, int? homeTeam, int? awayTeam)
        {
            return new Score(winner, homeTeam, awayTeam);
        }

        private Score(string winner, int? homeTeam, int? awayTeam)
        {
            Winner = winner;
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
        }
        
        public string Winner { get; }
        public int? HomeTeam { get; }
        public int? AwayTeam { get; }
    }
}
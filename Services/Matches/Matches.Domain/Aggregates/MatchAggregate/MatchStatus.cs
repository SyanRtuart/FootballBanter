using Base.Domain.SeedWork;

namespace Matches.Domain.Aggregates.MatchAggregate
{
    public class MatchStatus : Enumeration
    {
        public static MatchStatus Pending = new MatchStatus(1, nameof(Pending).ToLowerInvariant());
        public static MatchStatus InProgress = new MatchStatus(2, nameof(InProgress).ToLowerInvariant());
        public static MatchStatus Finished = new MatchStatus(3, nameof(Finished).ToLowerInvariant());
        public static MatchStatus Cancelled = new MatchStatus(4, nameof(Cancelled).ToLowerInvariant());

        public MatchStatus(int id, string name) : base(id, name)
        {
        }


    }
}

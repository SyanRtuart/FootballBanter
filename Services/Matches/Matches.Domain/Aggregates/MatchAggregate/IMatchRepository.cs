using System.Threading.Tasks;

namespace Matches.Domain.Aggregates.MatchAggregate
{
    public interface IMatchRepository
    {
        Task AddAsync(Match match);
    }
}

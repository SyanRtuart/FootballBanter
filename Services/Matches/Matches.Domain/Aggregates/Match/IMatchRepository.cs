using System.Threading.Tasks;
using Base.Domain.SeedWork;

namespace Matches.Domain.Aggregates.Match
{
    public interface IMatchRepository : IRepository<Match>
    {
        Task AddAsync(Match match);
    }
}
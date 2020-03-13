using System.Threading.Tasks;
using Base.Domain.SeedWork;

namespace Matches.Domain.Match
{
    public interface IMatchRepository : IRepository<Match>
    {
        Task AddAsync(Match match);
    }
}
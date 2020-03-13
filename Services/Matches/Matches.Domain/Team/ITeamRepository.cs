using System.Threading.Tasks;
using Base.Domain.SeedWork;

namespace Matches.Domain.Team
{
    public interface ITeamRepository : IRepository<Team>
    {
        Task AddAsync(Team team);
    }
}
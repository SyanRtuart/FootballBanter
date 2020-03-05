using System.Collections.Generic;
using System.Threading.Tasks;
using Base.Domain.SeedWork;

namespace Teams.Domain.Aggregates.TeamAggregate
{
    public interface ITeamRepository : IRepository<Team>
    {
        Team Add(Team team);

        Task<List<Team>> GetAllAsync();
    }
}

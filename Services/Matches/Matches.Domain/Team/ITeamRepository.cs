using System;
using System.Threading.Tasks;
using Base.Domain.SeedWork;

namespace Matches.Domain.Team
{
    public interface ITeamRepository
    {
        Task AddAsync(Team team);

        Task<Team> GetAsync(Guid id);
    }
}
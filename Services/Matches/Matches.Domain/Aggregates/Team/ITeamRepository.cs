using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Domain.SeedWork;

namespace Matches.Domain.Aggregates.Team
{
    public interface ITeamRepository : IRepository<Team>
    {
        Task AddAsync(Team team);
    }
}

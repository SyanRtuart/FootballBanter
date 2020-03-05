using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Domain.SeedWork;

namespace Teams.Domain.Aggregates
{
    public interface ITeamRepository : IRepository<Team>
    {
        Team Add(Team team);

        Task<List<Team>> GetAll();
    }
}

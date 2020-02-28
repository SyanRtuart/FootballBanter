using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teams.Domain.Aggregates;
using Teams.Domain.SeedWork;
using Teams.Infrastructure.Persistence;

namespace Teams.Infrastructure.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly TeamContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public TeamRepository(TeamContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Team Add(Team team)
        {
            return _context.Teams.Add(team).Entity;
        }
    }
}

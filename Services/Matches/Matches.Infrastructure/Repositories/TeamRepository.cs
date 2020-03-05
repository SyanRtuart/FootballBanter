using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.Domain.SeedWork;
using Matches.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Teams.Domain.Aggregates;
using Teams.Domain.Aggregates.TeamAggregate;

namespace Matches.Infrastructure.Repositories
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

        public Task<List<Team>> GetAll()
        {
            return _context.Teams.ToListAsync();
        }
    }
}

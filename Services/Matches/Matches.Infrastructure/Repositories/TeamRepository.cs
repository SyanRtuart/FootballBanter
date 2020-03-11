using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.Domain.SeedWork;
using Matches.Domain.Aggregates.Team;
using Matches.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Matches.Infrastructure.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly MatchContext _context;
        
        public IUnitOfWork UnitOfWork => _context;
        
        public TeamRepository(MatchContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(Team team)
        {
            await _context.Teams.AddAsync(team);
        }

    }
}

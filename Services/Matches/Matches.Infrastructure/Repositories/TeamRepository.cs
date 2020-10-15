using System;
using System.Linq;
using System.Threading.Tasks;
using Base.Domain.SeedWork;
using Matches.Domain.Team;
using Matches.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Matches.Infrastructure.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly MatchContext _context;

        public TeamRepository(MatchContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(Team team)
        {
            await _context.Teams.AddAsync(team);
        }

        public async Task<Team> GetAsync(TeamId id)
        {
            return await _context.Teams.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
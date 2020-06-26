using System;
using System.Threading.Tasks;
using Base.Domain.SeedWork;
using Matches.Domain.Team;
using Matches.Infrastructure.Persistence;

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
    }
}
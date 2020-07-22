using System;
using System.Threading.Tasks;
using Base.Domain.SeedWork;
using Matches.Domain.Match;
using Matches.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Matches.Infrastructure.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private readonly MatchContext _context;

        public MatchRepository(MatchContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(Match match)
        {
            await _context.Matches.AddAsync(match);
        }

        public async Task<Match> GetAsync(Guid id)
        {
            return await _context.Matches.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
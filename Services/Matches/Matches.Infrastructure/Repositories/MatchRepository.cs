using System;
using System.Threading.Tasks;
using Base.Domain.SeedWork;
using Matches.Domain.Match;
using Matches.Infrastructure.Persistence;

namespace Matches.Infrastructure.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private readonly MatchContext _context;

        public MatchRepository(MatchContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task AddAsync(Match match)
        {
            await _context.Matches.AddAsync(match);
        }
    }
}
using System;
using System.Threading.Tasks;
using Base.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Phrases.Domain.Phrase;
using Phrases.Infrastructure.Persistence;

namespace Phrases.Infrastructure.Repositories
{
    public class PhraseRepository : IPhraseRepository
    {
        private readonly PhraseContext _context;

        public PhraseRepository(PhraseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(Phrase phrase)
        {
            await _context.Phrases.AddAsync(phrase);
        }

        public async Task<Phrase> GetAsync(Guid phraseId)
        {
            return await _context.Phrases.FirstOrDefaultAsync(o => o.Id == phraseId);
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
using System;
using Base.Domain.SeedWork;
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

        public Phrase Add(Phrase phrase)
        {
            return _context.Phrases.Add(phrase).Entity;
        }

        public IUnitOfWork UnitOfWork => _context;
    }
}
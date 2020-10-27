using System;
using System.Threading.Tasks;
using Phrases.Domain.Members;
using Phrases.Infrastructure.Persistence;

namespace Phrases.Infrastructure.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly PhraseContext _context;

        public MemberRepository(PhraseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(Member member)
        {
            await _context.Members.AddAsync(member);
        }
    }
}

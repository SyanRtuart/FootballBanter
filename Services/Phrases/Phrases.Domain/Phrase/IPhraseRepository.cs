using System.Threading.Tasks;
using Base.Domain.SeedWork;

namespace Phrases.Domain.Phrase
{
    public interface IPhraseRepository : IRepository<Phrase>
    {
        Phrase Add(Phrase phrase);

        Task<Phrase> GetAsync(int phraseId);
    }
}
using System;
using System.Threading.Tasks;
using Base.Domain.SeedWork;

namespace Phrases.Domain.Phrase
{
    public interface IPhraseRepository 
    {
        Task AddAsync(Phrase phrase);

        Task<Phrase> GetAsync(PhraseId phraseId);

        Task<int> CommitAsync();
    }
}
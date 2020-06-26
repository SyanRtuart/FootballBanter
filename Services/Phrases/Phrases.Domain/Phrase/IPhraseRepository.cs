using System;
using System.Threading.Tasks;
using Base.Domain.SeedWork;

namespace Phrases.Domain.Phrase
{
    public interface IPhraseRepository 
    {
        Phrase Add(Phrase phrase);

        Task<Phrase> GetAsync(Guid phraseId);
    }
}
using Base.Domain.SeedWork;

namespace Phrases.Domain.Aggregates.PhraseAggregate
{
    public interface IPhraseRepository : IRepository<Phrase>
    {
        Phrase Add(Phrase phrase);
    }
}
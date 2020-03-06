using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Domain.SeedWork;

namespace Phrases.Domain.Aggregates.PhraseAggregate
{
    public interface IPhraseRepository : IRepository<Phrase>
    {
        Phrase Add(Phrase phrase);
    }
}

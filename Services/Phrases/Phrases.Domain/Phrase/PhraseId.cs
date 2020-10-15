using System;
using Base.Domain;

namespace Phrases.Domain.Phrase
{
    public class PhraseId : TypedIdValueBase
    {
        public PhraseId(Guid value) : base(value)
        {
        }
    }
}

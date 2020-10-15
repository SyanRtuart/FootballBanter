using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Domain;

namespace Phrases.Domain.Phrase
{
    public class PhraseVoteHistoryId : TypedIdValueBase
    {
        public PhraseVoteHistoryId(Guid value) : base(value)
        {
        }
    }
}

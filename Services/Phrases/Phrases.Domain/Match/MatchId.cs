using System;
using Base.Domain;

namespace Phrases.Domain.Match
{
    public class MatchId : TypedIdValueBase
    {
        public MatchId(Guid value) : base(value)
        {
        }
    }
}

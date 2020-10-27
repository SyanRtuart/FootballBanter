using System;
using Base.Domain;

namespace Phrases.Domain.Members
{
    public class MemberId : TypedIdValueBase
    {
        public MemberId(Guid value) : base(value)
        {
        }
    }
}

using System;
using Base.Domain;

namespace Phrases.Domain.User
{
    public class UserId : TypedIdValueBase
    {
        public UserId(Guid value) : base(value)
        {
        }
    }
}

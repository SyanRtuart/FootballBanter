using System;
using Base.Domain;

namespace Phrases.Domain.Team
{
    public class TeamId : TypedIdValueBase
    {
        public TeamId(Guid value) : base(value)
        {
        }
    }
}

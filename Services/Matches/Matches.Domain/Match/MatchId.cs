using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Domain;

namespace Matches.Domain.Match
{
    public class MatchId : TypedIdValueBase
    {
        public MatchId(Guid value) : base(value)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Domain;

namespace Matches.Domain.Team
{
    public class TeamId : TypedIdValueBase
    {
        public TeamId(Guid value) : base(value)
        {
        }
    }
}

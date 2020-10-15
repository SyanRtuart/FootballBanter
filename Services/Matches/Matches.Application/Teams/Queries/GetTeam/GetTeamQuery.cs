using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matches.Application.Configuration.Queries;

namespace Matches.Application.Teams.Queries.GetTeam
{
    public class GetTeamQuery : QueryBase<TeamDto>
    {
        public GetTeamQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}

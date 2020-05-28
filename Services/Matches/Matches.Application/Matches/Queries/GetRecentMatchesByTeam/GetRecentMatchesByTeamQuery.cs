using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matches.Application.Matches.SharedModels;
using MediatR;

namespace Matches.Application.Matches.Queries.GetRecentMatchesByTeam
{
    public class GetRecentMatchesByTeamQuery : IRequest<List<MatchDto>>
    {
        public GetRecentMatchesByTeamQuery(Guid teamId)
        {
            TeamId = teamId;
        }

        public Guid TeamId { get; }
    }
}

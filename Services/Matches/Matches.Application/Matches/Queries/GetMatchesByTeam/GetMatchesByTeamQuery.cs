using System;
using System.Collections.Generic;
using Matches.Application.Configuration.Queries;
using Matches.Application.Matches.SharedModels;

namespace Matches.Application.Matches.Queries.GetMatchesByTeam
{
    public class GetMatchesByTeamQuery : QueryBase<List<MatchDto>>
    {
        public GetMatchesByTeamQuery(Guid teamId)
        {
            TeamId = teamId;
        }

        public Guid TeamId { get; }
    }
}
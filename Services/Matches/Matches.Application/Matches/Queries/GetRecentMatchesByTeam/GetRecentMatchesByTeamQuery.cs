using System;
using System.Collections.Generic;
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
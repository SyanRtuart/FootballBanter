﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Matches.Application.Matches.Queries.GetRecentMatchesByTeam
{
    public class GetRecentMatchesByTeamQuery : IRequest<List<MatchDto>>
    {
        public GetRecentMatchesByTeamQuery(int teamId)
        {
            TeamId = teamId;
        }

        public int TeamId { get; }
    }
}
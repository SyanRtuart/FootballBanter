using System.Collections.Generic;
using Matches.Application.Configuration.Queries;
using Matches.Application.Contracts;
using MediatR;

namespace Matches.Application.Teams.Queries.GetAllTeams
{
    public class GetAllTeamsQuery : QueryBase<List<TeamDto>>
    {
    }
}
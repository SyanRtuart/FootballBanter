using System.Collections.Generic;
using MediatR;

namespace Matches.Application.Teams.Queries.GetAllTeams
{
    public class GetAllTeamsQuery : IRequest<List<TeamDto>>
    {


    }
}

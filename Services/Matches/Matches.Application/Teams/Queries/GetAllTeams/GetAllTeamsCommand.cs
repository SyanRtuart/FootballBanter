using System.Collections.Generic;
using MediatR;
using Teams.Domain.Aggregates;

namespace Matches.Application.Teams.Queries.GetAllTeams
{
    public class GetAllTeamsCommand : IRequest<TeamsViewModel>
    {


    }
}

using System.Collections.Generic;
using MediatR;
using Teams.Domain.Aggregates;
using Teams.Domain.Aggregates.TeamAggregate;

namespace Matches.Application.Teams.Queries.GetAllTeams
{
    public class GetAllTeamsCommand : IRequest<TeamsViewModel>
    {


    }
}

using System.Collections.Generic;
using MediatR;
using Teams.Domain.Aggregates;
using Teams.Domain.Aggregates.TeamAggregate;

namespace Matches.Application.Teams.Queries.GetAllTeams
{
    //TODO: Fix so it returns view model
    public class GetAllTeamsCommand : IRequest<List<Team>>
    {


    }
}

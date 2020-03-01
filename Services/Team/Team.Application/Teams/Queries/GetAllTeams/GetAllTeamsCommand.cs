using System.Collections.Generic;
using MediatR;
using Teams.Domain.Aggregates;

namespace Teams.Application.Teams.Queries.GetAllTeams
{
    //TODO: Fix so it returns view model
    public class GetAllTeamsCommand : IRequest<List<Team>>
    {


    }
}

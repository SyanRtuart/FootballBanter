using Base.Application.Common.Mappings;
using Teams.Domain.Aggregates.TeamAggregate;

namespace Matches.Application.Teams.Queries.GetAllTeams
{
    public class TeamDto : IMapFrom<Team>
    {
        public string Name { get; set; }
    }
}

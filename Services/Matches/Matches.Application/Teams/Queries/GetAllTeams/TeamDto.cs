using Base.Application.Common.Mappings;
using Matches.Domain.Aggregates.Match;

namespace Matches.Application.Teams.Queries.GetAllTeams
{
    public class TeamDto : IMapFrom<Team>
    {
        public string Name { get; set; }
    }
}

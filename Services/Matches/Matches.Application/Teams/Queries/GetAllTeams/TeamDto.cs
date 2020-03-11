using Base.Application.Common.Mappings;
using Matches.Domain.Aggregates.Match;
using Matches.Domain.Aggregates.Team;

namespace Matches.Application.Teams.Queries.GetAllTeams
{
    public class TeamDto : IMapFrom<Team>
    {
        public string Name { get; set; }
    }
}

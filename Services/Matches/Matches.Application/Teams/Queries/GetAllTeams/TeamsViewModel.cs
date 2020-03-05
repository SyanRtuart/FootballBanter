using System.Collections.Generic;

namespace Matches.Application.Teams.Queries.GetAllTeams
{
    public class TeamsViewModel
    {
        public TeamsViewModel(List<TeamDto> teams)
        {
            Teams = teams;
        }
        public List<TeamDto> Teams { get; private set; }
    }
}

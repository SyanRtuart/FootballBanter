using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Matches.Domain.Match;
using Matches.Domain.Team;

namespace Matches.Infrastructure.Persistence
{
    public class MatchContextInitializer
    {
        private readonly MatchContext _context;
        private readonly ITeamRepository _teamRepository;
        private List<Team> _teams;

        public MatchContextInitializer(MatchContext context, ITeamRepository teamRepository)
        {
            _context = context;
            _teamRepository = teamRepository;
        }

        public static void Initialize(MatchContext context, ITeamRepository teamRepository)
        {
            var initializer = new MatchContextInitializer(context, teamRepository);
            initializer.SeedEverything();
        }

        public async void SeedEverything()
        {
            if (!_context.Teams.Any())
            {
                SeedTeams();
                SeedMatches();

                await _context.SaveChangesAsync();
            }
        }

        private async void SeedTeams()
        {
            _teams = new List<Team>
            {
                Team.Create("Aberdeen"),
                Team.Create("Celtic"),
                Team.Create("Hamilton"),
                Team.Create("Hearts"),
                Team.Create("Hibernian"),
                Team.Create("Kilmarnock"),
                Team.Create("Livingston"),
                Team.Create("Motherwell"),
                Team.Create("Rangers"),
                Team.Create("Ross County"),
                Team.Create("St Johnstone"),
                Team.Create("St Mirren")
            };

            _context.Teams.AddRange(_teams);
        }

        private async void SeedMatches()
        {
            var winningGuid = _teams.FirstOrDefault().Id;
            var matches = new List<Match>();

            foreach (var team in _teams.ToList())
                matches.Add(Match.Create(winningGuid, team.Id, DateTime.Today.Subtract(TimeSpan.FromDays(2)),
                    new Score("Placeholder", 2, 1)));

            _context.Matches.AddRange(matches);
        }
    }
}
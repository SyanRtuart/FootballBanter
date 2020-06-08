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

        public MatchContextInitializer(MatchContext context)
        {
            _context = context;
        }

        public static void Initialize(MatchContext context)
        {
            var initializer = new MatchContextInitializer(context);
            initializer.SeedEverything();
        }

        public async void SeedEverything()
        {
            if (!_context.Teams.Any())
            {
                SeedTeams();
                SeedMatches();

                await _context.SaveEntitiesAsync(CancellationToken.None);
            }
        }

        private async void SeedTeams()
        {
            var teams = new List<Team>
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

            _context.Teams.AddRange(teams);
        }

        private async void SeedMatches()
        {
            var celticGuid = Guid.Parse("24521bf6-5467-4304-a6e5-14143330e605");
            var matches = new List<Match>();

            foreach (var team in _context.Teams.ToList())
                matches.Add(Match.Create(celticGuid, team.Id, DateTime.Today.Subtract(TimeSpan.FromDays(2)),
                    new Score("Celtic", 2, 1)));

            //var matches = new List<Match>
            //{
            //    Match.Create(homeTeamId, Guid.NewGuid(), DateTime.Today.Subtract(TimeSpan.FromDays(2)), new Score("Celtic", 2, 1)),
            //    Match.Create(homeTeamId, 3, DateTime.Today.Subtract(TimeSpan.FromDays(3)), new Score("Celtic", 2, 0)),
            //    Match.Create(homeTeamId, 4, DateTime.Today.Subtract(TimeSpan.FromDays(4)), new Score("Celtic", 3, 2)),
            //    Match.Create(homeTeamId, 5, DateTime.Today.Subtract(TimeSpan.FromDays(5)), new Score("Celtic", 1, 0)),
            //    Match.Create(homeTeamId, 6, DateTime.Today.Subtract(TimeSpan.FromDays(6)), new Score("Celtic", 1, 0)),
            //    Match.Create(homeTeamId, 7, DateTime.Today.Subtract(TimeSpan.FromDays(7)), new Score("Celtic", 1, 0)),
            //    Match.Create(homeTeamId, 8, DateTime.Today.Subtract(TimeSpan.FromDays(8)), new Score("Celtic", 2, 0)),
            //};

            _context.Matches.AddRange(matches);
        }
    }
}
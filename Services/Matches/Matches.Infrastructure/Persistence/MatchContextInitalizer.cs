using System;
using System.Collections.Generic;
using System.Threading;
using Matches.Domain.Match;
using Matches.Domain.Team;
using Microsoft.EntityFrameworkCore.Internal;

namespace Matches.Infrastructure.Persistence
{
    public class MatchContextInitalizer
    {
        private readonly MatchContext _context;

        public MatchContextInitalizer(MatchContext context)
        {
            _context = context;
        }

        public static void Initialize(MatchContext context)
        {
            var initilizer = new MatchContextInitalizer(context);
            initilizer.SeedEverything();
        }

        public void SeedEverything()
        {
            if (!_context.Teams.Any())
            {
                SeedTeams();
                SeedMatches();
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
            await _context.SaveEntitiesAsync(CancellationToken.None);
        }
        private async void SeedMatches()
        {
            var matches = new List<Match>
            {
                Match.Create(2, 1, DateTime.Today.Subtract(TimeSpan.FromDays(2)), new Score("Celtic", 2, 1)),
                Match.Create(2, 3, DateTime.Today.Subtract(TimeSpan.FromDays(3)), new Score("Celtic", 2, 0)),
                Match.Create(2, 4, DateTime.Today.Subtract(TimeSpan.FromDays(4)), new Score("Celtic", 3, 2)),
                Match.Create(2, 5, DateTime.Today.Subtract(TimeSpan.FromDays(5)), new Score("Celtic", 1, 0)),
                Match.Create(2, 6, DateTime.Today.Subtract(TimeSpan.FromDays(6)), new Score("Celtic", 1, 0)),
                Match.Create(2, 7, DateTime.Today.Subtract(TimeSpan.FromDays(7)), new Score("Celtic", 1, 0)),
                Match.Create(2, 8, DateTime.Today.Subtract(TimeSpan.FromDays(8)), new Score("Celtic", 2, 0)),
            };

            _context.Matches.AddRange(matches);
            await _context.SaveEntitiesAsync(CancellationToken.None);
        }
    }
}
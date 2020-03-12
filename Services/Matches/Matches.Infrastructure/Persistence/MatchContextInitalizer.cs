using System.Collections.Generic;
using System.Threading;
using Matches.Domain.Aggregates.Team;
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
            if (!_context.Teams.Any()) SeedTeams();
        }

        private void SeedTeams()
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
            _context.SaveEntitiesAsync(CancellationToken.None);
        }
    }
}
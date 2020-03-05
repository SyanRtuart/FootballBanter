using System.Collections.Generic;
using System.Threading;
using Teams.Domain.Aggregates;
using Teams.Domain.Aggregates.TeamAggregate;

namespace Matches.Infrastructure.Persistence
{
    public class MatchContextInitalizer
    {
        private MatchContext _context;

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
            SeedTeams();
        }

        private void SeedTeams()
        {
            var teams = new List<Team>()
            {
                new Team("Aberdeen"),
                new Team("Celtic"),
                new Team("Hamilton"),
                new Team("Hearts"),
                new Team("Hibernian"),
                new Team("Kilmarnock"),
                new Team("Livingston"),
                new Team("Motherwell"),
                new Team("Rangers"),
                new Team("Ross County"),
                new Team("St Johnstone"),
                new Team("St Mirren"),
            };

            _context.Teams.AddRange(teams);
            _context.SaveEntitiesAsync(CancellationToken.None);
        }
    }
}

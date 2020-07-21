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
            
        }

        private async void SeedMatches()
        {
        
        }
    }
}
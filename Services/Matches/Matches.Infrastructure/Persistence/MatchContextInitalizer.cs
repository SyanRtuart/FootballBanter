﻿using System.Collections.Generic;
using System.Threading;
using Matches.Domain.Aggregates.MatchAggregate;
using Microsoft.EntityFrameworkCore.Internal;

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
            if (!_context.Teams.Any())
            {
                SeedTeams();

            }
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
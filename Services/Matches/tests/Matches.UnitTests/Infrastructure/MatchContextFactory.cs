using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matches.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Matches.UnitTests.Infrastructure
{
    public class MatchContextFactory
    {
        public static MatchContext Create()
        {
            var options = new DbContextOptionsBuilder<MatchContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new MatchContext(options, new NoMediator());

            context.Database.EnsureCreated();

            return context;
        }

        public static void Destroy(MatchContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }

    }
}

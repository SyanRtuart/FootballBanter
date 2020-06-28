using System;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Base.Domain.SeedWork;
using Base.Infrastructure.Extensions;
using Base.Infrastructure.Inbox;
using Base.Infrastructure.InternalCommands;
using Base.Infrastructure.Outbox;
using Matches.Domain.Match;
using Matches.Domain.Team;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Matches.Infrastructure.Persistence
{
    public class MatchContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "Match";

        private readonly ILoggerFactory _loggerFactory;

        private MatchContext(DbContextOptions<MatchContext> options) : base(options)
        {
        }

        public MatchContext(DbContextOptions<MatchContext> options, ILoggerFactory loggerFactory) :
            base(options)
        {
            _loggerFactory = loggerFactory;

            Debug.WriteLine("UserAccessContext::ctor ->" + GetHashCode());
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }
        public DbSet<InternalCommand> InternalCommands { get; set; }
        public DbSet<InboxMessage> InboxMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MatchContext).Assembly);
        }
    }

    public class MatchContextDesignFactory : IDesignTimeDbContextFactory<MatchContext>
    {
        public MatchContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MatchContext>()
                .UseSqlServer(
                    "Data Source=database-1.cqlahoaopgco.eu-west-1.rds.amazonaws.com,1433;User ID=admin;Password=hamish123;database=FootballBanter;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            return new MatchContext(optionsBuilder.Options, new NullLoggerFactory());
        }
    }
}
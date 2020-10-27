using System;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Base.Domain.SeedWork;
using Base.Infrastructure;
using Base.Infrastructure.Extensions;
using Base.Infrastructure.Inbox;
using Base.Infrastructure.InternalCommands;
using Base.Infrastructure.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Phrases.Domain.Members;
using Phrases.Domain.Phrase;

namespace Phrases.Infrastructure.Persistence
{
    public class PhraseContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "Phrase";

        private readonly ILoggerFactory _loggerFactory;

        private PhraseContext(DbContextOptions<PhraseContext> options) : base(options)
        {
        }

        public PhraseContext(DbContextOptions<PhraseContext> options, ILoggerFactory loggerFactory) :
            base(options)
        {
            _loggerFactory = loggerFactory;

            Debug.WriteLine("UserAccessContext::ctor ->" + GetHashCode());
        }

        public DbSet<Phrase> Phrases { get; set; }

        public DbSet<Member> Members { get; set; }

        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        public DbSet<InternalCommand> InternalCommands { get; set; }

        public DbSet<InboxMessage> InboxMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PhraseContext).Assembly);
        }
    }

    //TODO Cleanup, string password used
    public class PhraseContextDesignFactory : IDesignTimeDbContextFactory<PhraseContext>
    {
        public PhraseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PhraseContext>()
                .UseSqlServer(
                    "Data Source=database-1.cqlahoaopgco.eu-west-1.rds.amazonaws.com,1433;User ID=admin;Password=hamish123;database=FootballBanter;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            optionsBuilder.ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();

            return new PhraseContext(optionsBuilder.Options, new NullLoggerFactory());
        }
    }
}
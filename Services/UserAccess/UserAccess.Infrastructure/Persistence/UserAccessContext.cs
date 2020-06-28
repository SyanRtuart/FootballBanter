using System.Diagnostics;
using Base.Infrastructure.Inbox;
using Base.Infrastructure.InternalCommands;
using Base.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using UserAccess.Domain.UserRegistrations;
using UserAccess.Domain.Users;

namespace UserAccess.Infrastructure.Persistence
{
    public class UserAccessContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "user";

        private readonly ILoggerFactory _loggerFactory;

        private UserAccessContext(DbContextOptions<UserAccessContext> options) : base(options)
        {
        }

        public UserAccessContext(DbContextOptions<UserAccessContext> options, ILoggerFactory loggerFactory) :
            base(options)
        {
            _loggerFactory = loggerFactory;

            Debug.WriteLine("UserAccessContext::ctor ->" + GetHashCode());
        }

        public DbSet<UserRegistration> UserRegistrations { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        public DbSet<InternalCommand> InternalCommands { get; set; }

        public DbSet<InboxMessage> InboxMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserAccessContext).Assembly);
        }
    }

    public class MatchContextDesignFactory : IDesignTimeDbContextFactory<UserAccessContext>
    {
        public UserAccessContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UserAccessContext>()
                .UseSqlServer(
                    "Data Source=database-1.cqlahoaopgco.eu-west-1.rds.amazonaws.com,1433;User ID=admin;Password=hamish123;database=FootballBanter;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            return new UserAccessContext(optionsBuilder.Options, new NullLoggerFactory());
        }
       
    }
}
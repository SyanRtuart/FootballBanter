using System.Diagnostics;
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

        //private class NoMediator : IMediator
        //{
        //    public Task Publish<TNotification>(TNotification notification,
        //        CancellationToken cancellationToken = default) where TNotification : INotification
        //    {
        //        return Task.CompletedTask;
        //    }

        //    public Task Publish(object notification, CancellationToken cancellationToken = default)
        //    {
        //        return Task.CompletedTask;
        //    }

        //    public Task<TResponse> Send<TResponse>(IRequest<TResponse> request,
        //        CancellationToken cancellationToken = default)
        //    {
        //        return Task.FromResult(default(TResponse));
        //    }

        //    public Task<object> Send(object request, CancellationToken cancellationToken = default)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public Task Send(IRequest request, CancellationToken cancellationToken = default)
        //    {
        //        return Task.CompletedTask;
        //    }
        //}
    }
}
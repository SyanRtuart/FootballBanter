using System;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Base.Domain.SeedWork;
using Base.Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;
using Phrases.Domain.Phrase;

namespace Phrases.Infrastructure.Persistence
{
    public class PhraseContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "phrase";

        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;

        private PhraseContext(DbContextOptions<PhraseContext> options) : base(options)
        {
        }

        public PhraseContext(DbContextOptions<PhraseContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));


            Debug.WriteLine("OrderingContext::ctor ->" + GetHashCode());
        }

        public DbSet<Phrase> Phrases { get; set; }

        public bool HasActiveTransaction => _currentTransaction != null;

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(this);

            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        public IDbContextTransaction GetCurrentTransaction()
        {
            return _currentTransaction;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PhraseContext).Assembly);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction)
                throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }

    public class PhraseContextDesignFactory : IDesignTimeDbContextFactory<PhraseContext>
    {
        public PhraseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PhraseContext>()
                .UseSqlServer("server=(localdb)\\MSSQLLocalDB;database=FootballBanter;trusted_connection=true;");

            return new PhraseContext(optionsBuilder.Options, new NoMediator());
        }

        private class NoMediator : IMediator
        {
            public Task Publish<TNotification>(TNotification notification,
                CancellationToken cancellationToken = default) where TNotification : INotification
            {
                return Task.CompletedTask;
            }

            public Task Publish(object notification, CancellationToken cancellationToken = default)
            {
                return Task.CompletedTask;
            }

            public Task<TResponse> Send<TResponse>(IRequest<TResponse> request,
                CancellationToken cancellationToken = default)
            {
                return Task.FromResult(default(TResponse));
            }

            public Task<object> Send(object request, CancellationToken cancellationToken = default)
            {
                throw new NotImplementedException();
            }

            public Task Send(IRequest request, CancellationToken cancellationToken = default)
            {
                return Task.CompletedTask;
            }
        }
    }
}
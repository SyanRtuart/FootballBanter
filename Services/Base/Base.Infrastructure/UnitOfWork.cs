﻿using System.Threading;
using System.Threading.Tasks;
using Base.Domain.SeedWork;
using Base.Infrastructure.DomainEventsDispatching;
using Microsoft.EntityFrameworkCore;

namespace Base.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;

        public UnitOfWork(
            DbContext context,
            IDomainEventsDispatcher domainEventsDispatcher)
        {
            _context = context;
            _domainEventsDispatcher = domainEventsDispatcher;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            await _domainEventsDispatcher.DispatchEventsAsync();
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
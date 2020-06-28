using System.Collections.Generic;
using System.Linq;
using Base.Domain;
using Base.Domain.SeedWork;
using Base.Infrastructure.DomainEventsDispatching;
using Phrases.Infrastructure.Persistence;

namespace Phrases.Infrastructure.DomainEventsDispatching
{
    public class DomainEventsAccessor : IDomainEventsAccessor
    {
        private readonly PhraseContext _context;

        public DomainEventsAccessor(PhraseContext context)
        {
            _context = context;
        }

        public List<IDomainEvent> GetAllDomainEvents()
        {
            var domainEntities = _context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            return domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();
        }

        public void ClearAllDomainEvents()
        {
            var domainEntities = _context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            domainEntities
                .ForEach(entity => entity.Entity.ClearDomainEvents());
        }
    }
}
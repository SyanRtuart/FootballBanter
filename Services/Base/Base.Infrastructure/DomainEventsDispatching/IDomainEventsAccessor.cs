using System.Collections.Generic;
using Base.Domain;

namespace Base.Infrastructure.DomainEventsDispatching
{
    public interface IDomainEventsAccessor
    {
        List<IDomainEvent> GetAllDomainEvents();

        void ClearAllDomainEvents();
    }
}
using System;
using Base.Domain;

namespace Base.Application.Events
{
    public class DomainNotificationBase<T> : IDomainEventNotification<T> where T : IDomainEvent
    {
        public DomainNotificationBase(T domainEvent)
        {
            Id = Guid.NewGuid();
            DomainEvent = domainEvent;
        }

        public T DomainEvent { get; }

        public Guid Id { get; }
    }
}
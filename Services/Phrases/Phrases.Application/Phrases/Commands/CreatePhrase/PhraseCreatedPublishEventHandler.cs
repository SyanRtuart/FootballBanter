using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Base.Infrastructure.EventBus;
using MediatR;
using Phrases.IntegrationEvents;

namespace Phrases.Application.Phrases.Commands.CreatePhrase
{
    public class PhraseCreatedPublishEventHandler : INotificationHandler<PhraseCreatedNotification>
    {
        private readonly IEventsBus _eventsBus;

        public PhraseCreatedPublishEventHandler(IEventsBus eventsBus)
        {
            _eventsBus = eventsBus;
        }


        public Task Handle(PhraseCreatedNotification notification, CancellationToken cancellationToken)
        {
            _eventsBus.Publish(new PhraseCreatedIntegrationEvent(notification.Id, notification.DomainEvent.OccurredOn,
                notification.DomainEvent.PhraseId,
                notification.DomainEvent.MatchId,
                notification.DomainEvent.TeamId,
                notification.DomainEvent.CreatedByUserId,
                notification.DomainEvent.Description,
                notification.DomainEvent.Positive,
                notification.DomainEvent.UtcDateCreated));

            return Task.CompletedTask;
        }
    }
}

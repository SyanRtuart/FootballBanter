using System.Threading;
using System.Threading.Tasks;
using Base.Infrastructure.EventBus;
using MediatR;
using Phrases.IntegrationEvents;

namespace Phrases.Application.Members.Commands.EditMemberGeneralAttributes
{
    public class MemberGeneralAttributesEditedPublishEventHandler : INotificationHandler<MemberGeneralAttributesEditedNotification>
    {
        private readonly IEventsBus _eventsBus;

        public MemberGeneralAttributesEditedPublishEventHandler(IEventsBus eventsBus)
        {
            _eventsBus = eventsBus;
        }

        public Task Handle(MemberGeneralAttributesEditedNotification notification, CancellationToken cancellationToken)
        {
            _eventsBus.Publish(new MemberGeneralAttributesEditedIntegrationEvent(notification.Id,
                notification.DomainEvent.OccurredOn,
                notification.DomainEvent.MemberId,
                notification.DomainEvent.FirstName,
                notification.DomainEvent.LastName,
                notification.DomainEvent.Picture));

            return Task.CompletedTask;
        }
    }
}

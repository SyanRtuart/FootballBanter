using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace UserAccess.Application.UserRegistrations.Commands.RegisterNewUser
{
    public class NewUserRegisteredPublishEventHandler : INotificationHandler<NewUserRegisteredNotification>
    {
        //private readonly IEventsBus _eventsBus;

        public Task Handle(NewUserRegisteredNotification notification, CancellationToken cancellationToken)
        {
            //_eventsBus.Publish(new NewUserRegisteredIntegrationEvent(notification.Id, notification.DomainEvent.OccurredOn,
            //    notification.DomainEvent.UserRegistrationId.Value,
            //    notification.DomainEvent.Login,
            //    notification.DomainEvent.Email,
            //    notification.DomainEvent.FirstName,
            //    notification.DomainEvent.LastName,
            //    notification.DomainEvent.Name));

            return Task.CompletedTask;
        }
    }
}
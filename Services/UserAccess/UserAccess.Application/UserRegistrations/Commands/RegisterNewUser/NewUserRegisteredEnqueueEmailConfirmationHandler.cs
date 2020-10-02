using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserAccess.Application.Configuration.Commands;
using UserAccess.Application.UserRegistrations.Commands.SendUserRegistrationConfirmationEmail;

namespace UserAccess.Application.UserRegistrations.Commands.RegisterNewUser
{
    public class NewUserRegisteredEnqueueEmailConfirmationHandler : INotificationHandler<NewUserRegisteredNotification>
    {
        private readonly ICommandsScheduler _commandsScheduler;

        public NewUserRegisteredEnqueueEmailConfirmationHandler(ICommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }

        public async Task Handle(NewUserRegisteredNotification notification, CancellationToken cancellationToken)
        {
            await _commandsScheduler.EnqueueAsync(new SendUserRegistrationConfirmationEmailCommand(
                Guid.NewGuid(),
                notification.DomainEvent.UserRegistrationId,
                notification.DomainEvent.Email));
        }
    }
}

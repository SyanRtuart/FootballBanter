using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Phrases.IntegrationEvents;
using UserAccess.Application.Configuration.Commands;

namespace UserAccess.Application.Users.Commands.EditUserGeneralAttributes
{
    public class MemberGeneralAttributesEditedIntegrationEventHandler : INotificationHandler<MemberGeneralAttributesEditedIntegrationEvent>
    {
        private readonly ICommandsScheduler _commandsScheduler;

        public MemberGeneralAttributesEditedIntegrationEventHandler(ICommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }

        public Task Handle(MemberGeneralAttributesEditedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            _commandsScheduler.EnqueueAsync(new EditUserGeneralAttributesCommand(notification.Id, notification.MemberId,
                notification.FirstName, notification.LastName));

            return Task.CompletedTask;
        }
    }
}

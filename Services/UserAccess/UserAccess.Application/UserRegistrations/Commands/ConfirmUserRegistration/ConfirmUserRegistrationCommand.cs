using System;
using MediatR;

namespace UserAccess.Application.UserRegistrations.Commands.ConfirmUserRegistration
{
    public class ConfirmUserRegistrationCommand : IRequest
    {
        public ConfirmUserRegistrationCommand(Guid userRegistrationId)
        {
            UserRegistrationId = userRegistrationId;
        }

        public Guid UserRegistrationId { get; }
    }
}
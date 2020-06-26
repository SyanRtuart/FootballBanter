using System;
using MediatR;
using UserAccess.Application.Contracts;

namespace UserAccess.Application.UserRegistrations.Commands.ConfirmUserRegistration
{
    public class ConfirmUserRegistrationCommand : CommandBase<Unit>
    {
        public ConfirmUserRegistrationCommand(Guid userRegistrationId)
        {
            UserRegistrationId = userRegistrationId;
        }

        public Guid UserRegistrationId { get; }
    }
}
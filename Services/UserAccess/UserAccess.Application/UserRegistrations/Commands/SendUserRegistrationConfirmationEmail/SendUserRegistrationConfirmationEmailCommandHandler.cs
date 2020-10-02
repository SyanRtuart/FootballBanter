using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Base.Application.Emails;
using MediatR;
using UserAccess.Application.Configuration.Commands;

namespace UserAccess.Application.UserRegistrations.Commands.SendUserRegistrationConfirmationEmail
{
    internal class SendUserRegistrationConfirmationEmailCommandHandler : ICommandHandler<SendUserRegistrationConfirmationEmailCommand>
    {
        private readonly IEmailSender _emailSender;

        public SendUserRegistrationConfirmationEmailCommandHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public Task<Unit> Handle(SendUserRegistrationConfirmationEmailCommand request, CancellationToken cancellationToken)
        {
            var emailMessage = new EmailMessage(request.Email, "FootballBanter - Please confirm your registration",
                "This should be link to confirmation page. For now, please execute HTTP request " +
                $"PATCH http://localhost:5000/userAccess/userRegistrations/{request.UserRegistrationId}/confirm");

            _emailSender.SendEmail(emailMessage);

            return Unit.Task;
        }
    }
}

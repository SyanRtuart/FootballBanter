using System;
using System.Threading;
using System.Threading.Tasks;
using Base.Application.Emails;
using MediatR;
using UserAccess.Application.Authentication;
using UserAccess.Application.Configuration.Commands;
using UserAccess.Domain.UserRegistrations;

namespace UserAccess.Application.UserRegistrations.Commands.RegisterNewUser
{
    public class RegisterNewUserCommandHandler : ICommandHandler<RegisterNewUserCommand, Guid>
    {
        private readonly IEmailSender _emailSender;
        private readonly IUserRegistrationRepository _userRegistrationRepository;
        private readonly IUsersCounter _usersCounter;

        public RegisterNewUserCommandHandler(
            IUserRegistrationRepository userRegistrationRepository,
            IUsersCounter usersCounter, IEmailSender emailSender)
        {
            _userRegistrationRepository = userRegistrationRepository;
            _usersCounter = usersCounter;
            _emailSender = emailSender;
        }

        public async Task<Guid> Handle(RegisterNewUserCommand request, CancellationToken cancellationToken)
        {
            var password = PasswordManager.HashPassword(request.Password);

            var registration = UserRegistration.RegisterNewUser(
                request.Login,
                password,
                request.Email,
                request.FirstName,
                request.LastName,
                _usersCounter);

            await _userRegistrationRepository.AddAsync(registration);

            var email = new EmailMessage(request.Email,
                "Football Banter - Please confirm your registration",
                "This should be link to confirmation page. For now, please execute HTTP request " +
                $"PATCH http://localhost:5000/userRegistration/{registration.Id.Value}/confirm");

            await _emailSender.SendEmail(email);

            return registration.Id.Value;
        }
    }
}
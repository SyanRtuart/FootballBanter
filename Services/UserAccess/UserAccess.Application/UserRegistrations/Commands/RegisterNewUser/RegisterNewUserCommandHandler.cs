using System;
using System.Threading;
using System.Threading.Tasks;
using Base.Application.Emails;
using MediatR;
using UserAccess.Application.Authentication;
using UserAccess.Domain.UserRegistrations;
using UserAccess.Domain.UserRegistrations.Events;

namespace UserAccess.Application.UserRegistrations.Commands.RegisterNewUser
{
    public class RegisterNewUserCommandHandler : IRequestHandler<RegisterNewUserCommand, Guid>
    {
        private readonly IUserRegistrationRepository _userRegistrationRepository;
        private readonly IUsersCounter _usersCounter;
        private readonly IEmailSender _emailSender;
        private readonly IMediator _mediator;

        public RegisterNewUserCommandHandler(
            IUserRegistrationRepository userRegistrationRepository,
            IUsersCounter usersCounter, IEmailSender emailSender, IMediator mediator)
        {
            _userRegistrationRepository = userRegistrationRepository;
            _usersCounter = usersCounter;
            _emailSender = emailSender;
            _mediator = mediator;
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

            //await _mediator.Send(new NewUserRegisteredNotification(
            //     new NewUserRegisteredDomainEvent(Guid.NewGuid(), "2", "2", "", "", "", DateTime.Now)));

            var email = new EmailMessage(request.Email,
                "Football Banter - Please confirm your registration",  
                "This should be link to confirmation page. For now, please execute HTTP request " +
                $"PATCH http://localhost:5000/userAccess/userRegistrations/{registration.Id}/confirm");

            await _emailSender.SendEmail(email);

            //await _userRegistrationRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);


            return registration.Id;
        }
    }
}
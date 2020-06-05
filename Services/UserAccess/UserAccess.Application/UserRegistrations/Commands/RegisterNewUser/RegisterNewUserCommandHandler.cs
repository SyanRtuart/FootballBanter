using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserAccess.Application.Authentication;
using UserAccess.Domain.UserRegistrations;

namespace UserAccess.Application.UserRegistrations.Commands.RegisterNewUser
{
    public class RegisterNewUserCommandHandler : IRequestHandler<RegisterNewUserCommand, Guid>
    {
        private readonly IUserRegistrationRepository _userRegistrationRepository;
        private readonly IUsersCounter _usersCounter;

        public RegisterNewUserCommandHandler(
            IUserRegistrationRepository userRegistrationRepository,
            IUsersCounter usersCounter)
        {
            _userRegistrationRepository = userRegistrationRepository;
            _usersCounter = usersCounter;
        }

        public async Task<Guid> Handle(RegisterNewUserCommand request, CancellationToken cancellationToken)
        {
            var password = PasswordManager.HashPassword(request.Password);

            var userRegistration = UserRegistration.RegisterNewUser(
                request.Login,
                password,
                request.Email,
                request.FirstName,
                request.LastName,
                _usersCounter);

            await _userRegistrationRepository.AddAsync(userRegistration);

            return userRegistration.Id;
        }
    }
}


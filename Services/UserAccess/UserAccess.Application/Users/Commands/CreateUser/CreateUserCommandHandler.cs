using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserAccess.Application.Configuration.Commands;
using UserAccess.Domain.UserRegistrations;
using UserAccess.Domain.Users;

namespace UserAccess.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Guid>
    {
        private readonly IUserRegistrationRepository _userRegistrationRepository;
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRegistrationRepository userRegistrationRepository, IUserRepository userRepository)
        {
            _userRegistrationRepository = userRegistrationRepository;
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userRegistration = await _userRegistrationRepository.GetByIdAsync(request.UserRegistrationId);

            var user = userRegistration.CreateUser();

            await _userRepository.AddAsync(user);

            return user.Id;
        }
    }
}

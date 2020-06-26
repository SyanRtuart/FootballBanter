using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserAccess.Application.Configuration.Commands;
using UserAccess.Domain.UserRegistrations;

namespace UserAccess.Application.UserRegistrations.Commands.ConfirmUserRegistration
{
    public class ConfirmUserRegistrationCommandHandler : ICommandHandler<ConfirmUserRegistrationCommand, Unit>
    {
        private readonly IUserRegistrationRepository _userRegistrationRepository;

        public ConfirmUserRegistrationCommandHandler(IUserRegistrationRepository userRegistrationRepository)
        {
            _userRegistrationRepository = userRegistrationRepository;
        }

        public async Task<Unit> Handle(ConfirmUserRegistrationCommand request, CancellationToken cancellationToken)
        {
            var userRegistration =
                await _userRegistrationRepository.GetByIdAsync(request.UserRegistrationId);

            userRegistration.Confirm();

            return Unit.Value;
        }
    }
}
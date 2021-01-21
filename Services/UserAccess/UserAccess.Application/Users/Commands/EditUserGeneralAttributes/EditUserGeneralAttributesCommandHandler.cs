using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserAccess.Application.Configuration.Commands;
using UserAccess.Domain.Users;

namespace UserAccess.Application.Users.Commands.EditUserGeneralAttributes
{
    public class EditUserGeneralAttributesCommandHandler : ICommandHandler<EditUserGeneralAttributesCommand>
    {
        private readonly IUserRepository _userRepository;

        public EditUserGeneralAttributesCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(EditUserGeneralAttributesCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(new UserId(request.UserId));

            user.EditGeneralAttributes(request.FirstName, request.LastName);

            await _userRepository.Commit();

            return Unit.Value;
        }
    }
}

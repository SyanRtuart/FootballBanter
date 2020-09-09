using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserAccess.Application.Configuration.Commands;
using UserAccess.Domain.Users;

namespace UserAccess.Application.Users.Commands.AddPicture
{
    public class AddPictureCommandHandler : ICommandHandler<AddPictureCommand>
    {
        private readonly IUserRepository _userRepository;

        public AddPictureCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(AddPictureCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(request.UserId);

            user.AddPicture(request.Picture);

            return Unit.Value;
        }
    }
}

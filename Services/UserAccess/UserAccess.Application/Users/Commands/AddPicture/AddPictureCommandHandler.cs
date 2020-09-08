using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserAccess.Application.Configuration.Commands;

namespace UserAccess.Application.Users.Commands.AddPicture
{
    public class AddPictureCommandHandler : ICommandHandler<AddPictureCommand>
    {

        public Task<Unit> Handle(AddPictureCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

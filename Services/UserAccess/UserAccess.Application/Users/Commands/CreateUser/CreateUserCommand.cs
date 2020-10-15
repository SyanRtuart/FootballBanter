using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAccess.Application.Configuration.Commands;
using UserAccess.Domain.UserRegistrations;

namespace UserAccess.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : InternalCommandBase<Guid>
    {
        public CreateUserCommand(Guid id, UserRegistrationId userRegistrationId) : base(id)
        {
            UserRegistrationId = userRegistrationId;
        }

        public UserRegistrationId UserRegistrationId { get; set; }
    }
}

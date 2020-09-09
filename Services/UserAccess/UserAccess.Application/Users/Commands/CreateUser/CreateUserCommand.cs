using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAccess.Application.Configuration.Commands;

namespace UserAccess.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : InternalCommandBase<Guid>
    {
        public CreateUserCommand(Guid id, Guid userRegistrationId) : base(id)
        {
            UserRegistrationId = userRegistrationId;
        }

        public Guid UserRegistrationId { get; set; }
    }
}

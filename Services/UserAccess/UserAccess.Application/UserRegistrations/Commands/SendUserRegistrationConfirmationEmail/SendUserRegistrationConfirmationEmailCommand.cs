using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UserAccess.Application.Configuration.Commands;
using UserAccess.Application.Contracts;

namespace UserAccess.Application.UserRegistrations.Commands.SendUserRegistrationConfirmationEmail
{
    public class SendUserRegistrationConfirmationEmailCommand : InternalCommandBase
    {
        [JsonConstructor]
        public SendUserRegistrationConfirmationEmailCommand(Guid id, Guid userRegistrationId, string email)
            : base(id)
        {
            UserRegistrationId = userRegistrationId;
            Email = email;
        }

        internal Guid UserRegistrationId { get; }

        internal string Email { get; }
    }
}

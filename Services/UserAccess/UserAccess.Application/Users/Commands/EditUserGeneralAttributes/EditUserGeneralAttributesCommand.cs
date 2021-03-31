using System;
using UserAccess.Application.Configuration.Commands;

namespace UserAccess.Application.Users.Commands.EditUserGeneralAttributes
{
    public class EditUserGeneralAttributesCommand : InternalCommandBase
    {
        public EditUserGeneralAttributesCommand(Guid id, Guid userId, string firstName, string lastName) : base(id)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
        }

        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

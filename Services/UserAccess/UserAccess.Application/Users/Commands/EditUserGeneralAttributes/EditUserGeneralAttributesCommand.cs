using System;
using UserAccess.Application.Contracts;

namespace UserAccess.Application.Users.Commands.EditUserGeneralAttributes
{
    public class EditUserGeneralAttributesCommand : CommandBase
    {
        public EditUserGeneralAttributesCommand(Guid userId, string firstName, string lastName, byte[] picture)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Picture = picture;
        }

        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Picture { get; set; }
    }
}

using System;
using UserAccess.Application.Contracts;

namespace UserAccess.Application.Users.Commands.ChangePassword
{
    public class ChangePasswordCommand : CommandBase
    {
        public ChangePasswordCommand(Guid userId, string currentPassword, string newPassword)
        {
            UserId = userId;
            CurrentPassword = currentPassword;
            NewPassword = newPassword;
        }

        public Guid UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}

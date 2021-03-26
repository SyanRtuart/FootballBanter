using System;

namespace UserAccess.Application.Users.Commands.ChangePassword
{
    public class ChangePasswordRequest
    {
        public Guid UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
     
    }
}

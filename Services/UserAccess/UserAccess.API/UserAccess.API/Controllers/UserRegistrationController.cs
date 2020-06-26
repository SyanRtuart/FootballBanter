using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserAccess.Application.Contracts;
using UserAccess.Application.UserRegistrations.Commands.ConfirmUserRegistration;
using UserAccess.Application.UserRegistrations.Commands.RegisterNewUser;

namespace UserAccess.API.Controllers
{
    [Route("[controller]")]
    public class UserRegistrationController : ControllerBase
    {
        private readonly IUserAccessModule _userAccessModule;

        public UserRegistrationController(IUserAccessModule userAccessModule)
        {
            _userAccessModule = userAccessModule;
        }

        [AllowAnonymous]
        [HttpPost("")]
        public async Task<IActionResult> RegisterNewUser([FromBody] RegisterNewUserRequest request)
        {
            await _userAccessModule.ExecuteCommandAsync(new RegisterNewUserCommand(request.Login, request.Password,
                request.Email,
                request.FirstName, request.LastName));

            return Ok();
        }

        [AllowAnonymous]
        [HttpPatch("{userRegistrationId}/confirm")]
        public async Task<IActionResult> ConfirmRegistration(Guid userRegistrationId)
        {
            await _userAccessModule.ExecuteCommandAsync(new ConfirmUserRegistrationCommand(userRegistrationId));

            return Ok();
        }
    }
}
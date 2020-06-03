using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserAccess.Application.UserRegistrations.ConfirmUserRegistration;
using UserAccess.Application.UserRegistrations.RegisterNewUser;

namespace UserAccess.API.Controllers
{
    [Route("[controller]")]
    public class UserRegistrationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserRegistrationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("")]
        public async Task<IActionResult> RegisterNewUser(RegisterNewUserRequest request)
        {
            await _mediator.Send(new RegisterNewUserCommand(request.Login, request.Password, request.Email,
                request.FirstName, request.LastName));

            return Ok();
        }

        [AllowAnonymous]
        [HttpPatch("{userRegistrationId}/confirm")]
        public async Task<IActionResult> ConfirmRegistration(Guid userRegistrationId)
        {
            await _mediator.Send(new ConfirmUserRegistrationCommand(userRegistrationId));

            return Ok();
        }
    }
}

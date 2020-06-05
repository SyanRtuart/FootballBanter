using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UserAccess.API.Configuration.Authorization;
using UserAccess.Application.Authentication.Authenticate;
using UserAccess.Application.UserRegistrations.Commands.ConfirmUserRegistration;
using UserAccess.Application.UserRegistrations.Commands.RegisterNewUser;

namespace UserAccess.API.Controllers
{
    [Route("[controller]")]
    public class UserRegistrationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserRegistrationController(IMediator mediator, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
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

        [HttpGet("rs")]
        [HasPermission(PaymentsPermissions.RegisterPayment)]
        public async Task<IActionResult> TestRs()
        {
            return Ok();
        }
    }

    public class PaymentsPermissions
    {
        public const string RegisterPayment = "RegisterPayment";
    }
}

﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserAccess.Application.Contracts;
using UserAccess.Application.Users.Commands.ChangePassword;
using UserAccess.Application.Users.Commands.EditUserGeneralAttributes;
using UserAccess.Application.Users.Queries.GetUserByEmail;

namespace UserAccess.API.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserAccessModule _userAccessModule;

        public UserController(IUserAccessModule userAccessModule)
        {
            _userAccessModule = userAccessModule;
        }

        [HttpGet]
        public async Task<UserDto> Get(string email)
        {
            return await _userAccessModule.ExecuteQueryAsync(new GetUserByEmailQuery(email));
        }

        [HttpPost("{userid}/changePassword")]
        public async Task<IActionResult> ChangePassword([FromRoute] Guid userId,
            [FromBody] ChangePasswordRequest request)
        {
            await _userAccessModule.ExecuteCommandAsync(new ChangePasswordCommand(
                userId, 
                request.CurrentPassword,
                request.NewPassword));

            return Ok();
        }
    }
}

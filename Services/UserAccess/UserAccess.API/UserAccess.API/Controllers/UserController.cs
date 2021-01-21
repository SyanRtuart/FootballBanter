using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserAccess.Application.Contracts;
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

        [HttpPut("{userId}")]
        public async Task<IActionResult> EditUserGeneralAttributes([FromRoute] Guid userId,
            [FromBody] EditUserGeneralAttributesCommand request)
        {
            await _userAccessModule.ExecuteCommandAsync(new EditUserGeneralAttributesCommand(
                userId,
                request.FirstName,
                request.LastName,
                request.Picture));

            return Ok();
        }
    }
}

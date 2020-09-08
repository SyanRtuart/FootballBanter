using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserAccess.Application.Contracts;
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

        [HttpPost]
        [Route("{id}/picture")]
        public async Task<IActionResult> AddPicture()
        {
            var httpRequest = HttpContext.Request;
            //Upload Image
            var postedFile = httpRequest.Form.Files["Image"];

            return Ok();
        }
    }
}

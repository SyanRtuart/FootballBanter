using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Web.HttpAggregator.Models.UserAccess;
using Web.HttpAggregator.Services;
using Web.HttpAggregator.Services.UserAccess;

namespace Web.HttpAggregator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserAccessController : ControllerBase
    {
        private readonly IUserAccessApiClient _userAccessApiClient;
        private readonly IExecutionContextAccessor _executionContextAccessor;

        public UserAccessController(IUserAccessApiClient userAccessApiClient, IExecutionContextAccessor executionContextAccessor)
        {
            _userAccessApiClient = userAccessApiClient;
            _executionContextAccessor = executionContextAccessor;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterNewUserRequest request)
        {
            await _userAccessApiClient.RegisterNewUserAsync(request);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var tokenResponse = await _userAccessApiClient.LoginAsync(request);

            if (tokenResponse.IsError) return BadRequest(tokenResponse.Error);

            return Ok(new LoginResult(tokenResponse.AccessToken, tokenResponse.ExpiresIn,
                tokenResponse.IssuedTokenType, tokenResponse.RefreshToken, tokenResponse.Scope));
        }

        [HttpGet]
        [Route("user")]
        public async Task<User> GetAccount(string email)
        {
            var response = await _userAccessApiClient.GetUser(email);

            var user = new User(response.FirstName, response.LastName, response.Email, response.Login);
                
            return user;
        }

        [HttpPost]
        [Route("uploadPicture")]
        public async Task<IActionResult> UploadImage()
        {
            var httpRequest = HttpContext.Request;

            var postedFile = httpRequest.Form.Files["Image"];

            //byte[] pictureAsBytes;
            //await using (var ms = new MemoryStream())
            //{
            //    postedFile.CopyTo(ms);
            //    pictureAsBytes = ms.ToArray();
            //}

            await _userAccessApiClient.UploadPicture(new UploadPictureRequest(_executionContextAccessor.UserId,
                postedFile));

            return Ok();
        }
    }
}
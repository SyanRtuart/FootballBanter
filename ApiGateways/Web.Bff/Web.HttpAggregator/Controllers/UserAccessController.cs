using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.HttpAggregator.Models;
using Web.HttpAggregator.Models.UserAccess;
using Web.HttpAggregator.Services.UserAccess;

namespace Web.HttpAggregator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserAccessController : ControllerBase
    {
        private readonly IUserAccessApiClient _userAccessApiClient;

        public UserAccessController(IUserAccessApiClient userAccessApiClient)
        {
            _userAccessApiClient = userAccessApiClient;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterNewUserRequest request)
        {
            await _userAccessApiClient.RegisterNewUserAsync(request);

            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var tokenResponse = await _userAccessApiClient.LoginAsync(request);

            if (tokenResponse.IsError) return BadRequest(tokenResponse.Error);

            return Ok(new LoginResult(tokenResponse.AccessToken, tokenResponse.ExpiresIn,
                tokenResponse.IssuedTokenType, tokenResponse.RefreshToken, tokenResponse.Scope));
        }
    }
}
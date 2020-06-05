using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Net.Http.Headers;
using UserAccess.Application.Users.Queries;

namespace UserAccess.API.Configuration.Authorization
{
    internal class HasPermissionAuthorizationHandler : AttributeAuthorizationHandler<HasPermissionAuthorizationRequirement, HasPermissionAttribute>
    {
        private readonly IMediator _mediator;
        //private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HasPermissionAuthorizationHandler(IMediator mediator, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, HasPermissionAuthorizationRequirement requirement, IEnumerable<HasPermissionAttribute> attributes)
        {
            var authorizationHeader = this._httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var tokenValue = authorizationHeader.First().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            
            var userId = Guid.Parse(handler.ReadJwtToken(tokenValue).Claims.SingleOrDefault(x => x.Type == "sub")?.Value ?? throw new InvalidOperationException());

            var permissions = await _mediator.Send(new GetUserPermissionsQuery(userId));
            
            foreach (var permissionAttribute in attributes)
            {
                if (!await AuthorizeAsync(permissionAttribute.Name, permissions))
                {
                    context.Fail();
                    return;
                }
            }

            context.Succeed(requirement);
        }

        private Task<bool> AuthorizeAsync(string permission, List<UserPermissionDto> permissions)
        {
#if !DEBUG
            return Task.FromResult(true);
#endif

            if (permissions.Any(x => x.Code == permission))
            {
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}

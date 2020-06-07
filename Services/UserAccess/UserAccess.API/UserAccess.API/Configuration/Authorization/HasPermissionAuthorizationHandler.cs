using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Base.Application.BuildingBlocks;
using IdentityModel.Client;
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
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HasPermissionAuthorizationHandler(IMediator mediator, IHttpContextAccessor httpContextAccessor, IExecutionContextAccessor executionContextAccessor) : base(httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
            _executionContextAccessor = executionContextAccessor;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, HasPermissionAuthorizationRequirement requirement, IEnumerable<HasPermissionAttribute> attributes)
        {
            var permissions = _executionContextAccessor.UserPermissions;
            
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

        private Task<bool> AuthorizeAsync(string permission, List<string> permissions)
        {
#if !DEBUG
            return Task.FromResult(true);
#endif

            if (permissions.Any(x => x == permission))
            {
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}

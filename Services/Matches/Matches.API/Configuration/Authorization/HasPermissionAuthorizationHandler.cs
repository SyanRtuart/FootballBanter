using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Application.BuildingBlocks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Matches.API.Configuration.Authorization
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
            var userPermissions = _executionContextAccessor.UserPermissions;

            foreach (var permissionAttribute in attributes)
            {
                if (!await AuthorizeAsync(permissionAttribute.Name, userPermissions))
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

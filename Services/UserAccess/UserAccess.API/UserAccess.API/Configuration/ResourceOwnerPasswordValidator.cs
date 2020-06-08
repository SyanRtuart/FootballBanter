using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Base.Application.Permissions;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using MediatR;
using UserAccess.Application.Authentication.Authenticate;
using UserAccess.Application.Users.Queries;

namespace UserAccess.API.Configuration
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IMediator _mediator;

        public ResourceOwnerPasswordValidator(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var authenticationResult = await _mediator.Send(new AuthenticateCommand(context.UserName, context.Password));
            if (!authenticationResult.IsAuthenticated)
            {
                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidGrant,
                    authenticationResult.AuthenticationError);
                return;
            }

            var userPermissions = await _mediator.Send(new GetUserPermissionsQuery(authenticationResult.User.Id));

            foreach (var permission in userPermissions)
            {
                authenticationResult.User.Claims.Add(new Claim(CustomClaimTypes.Permission, permission.Code));
            }

            context.Result = new GrantValidationResult(
                authenticationResult.User.Id.ToString(),
                "forms",
                authenticationResult.User.Claims);

        }
    }
}

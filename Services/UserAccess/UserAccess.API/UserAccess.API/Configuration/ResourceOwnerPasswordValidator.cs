using System.Security.Claims;
using System.Threading.Tasks;
using Base.Application.Permissions;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using MediatR;
using UserAccess.Application.Authentication.Authenticate;
using UserAccess.Application.Contracts;
using UserAccess.Application.Users.Queries;
using UserAccess.Application.Users.Queries.GetUserPermissions;

namespace UserAccess.API.Configuration
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserAccessModule _userAccessModule;

        public ResourceOwnerPasswordValidator(IUserAccessModule userAccessModule)
        {
            _userAccessModule = userAccessModule;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var authenticationResult =
                await _userAccessModule.ExecuteCommandAsync(new AuthenticateCommand(context.UserName, context.Password));
            if (!authenticationResult.IsAuthenticated)
            {
                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidGrant,
                    authenticationResult.AuthenticationError);
                return;
            }

            var userPermissions = await _userAccessModule.ExecuteQueryAsync(new GetUserPermissionsQuery(authenticationResult.User.Id));

            foreach (var permission in userPermissions)
                authenticationResult.User.Claims.Add(new Claim(CustomClaimTypes.Permission, permission.Code));

            context.Result = new GrantValidationResult(
                authenticationResult.User.Id.ToString(),
                "forms",
                authenticationResult.User.Claims);
        }
    }
}
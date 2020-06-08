using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Application.BuildingBlocks;
using Base.Application.Permissions;
using Microsoft.AspNetCore.Http;

namespace Matches.API.Configuration
{
    public class ExecutionContextAccessor : IExecutionContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExecutionContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId
        {
            get
            {
                if (_httpContextAccessor
                    .HttpContext?
                    .User?
                    .Claims?
                    .SingleOrDefault(x => x.Type == "sub")?
                    .Value != null)

                {
                    return Guid.Parse(_httpContextAccessor.HttpContext.User.Claims.Single(x => x.Type == "sub").Value);

                }

                throw new ApplicationException("User context is not available");
            }
        }

        public Guid CorrelationId { get; }
        public bool IsAvailable { get; }
        public List<string> UserPermissions
        {
            get
            {

                var permissionClaims = _httpContextAccessor
                    .HttpContext?
                    .User?
                    .Claims?
                    .Where(x => x.Type == CustomClaimTypes.Permission)
                    .ToList();

                var userPermissions = new List<string>();
                if (permissionClaims != null)
                {
                    foreach (var permission in permissionClaims)
                    {
                        userPermissions.Add(permission.Value);
                    }
                }

                return userPermissions;
            }
        }
    }
}

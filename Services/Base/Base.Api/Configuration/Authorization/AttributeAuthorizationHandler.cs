using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Base.Api.Configuration.Authorization
{
    public abstract class AttributeAuthorizationHandler<TRequirement, TAttribute> : AuthorizationHandler<TRequirement>
        where TRequirement : IAuthorizationRequirement where TAttribute : Attribute
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        protected AttributeAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TRequirement requirement)
        {
            var attributes = new List<TAttribute>();

            if (context.Resource is Endpoint endpoint)
            {
                var controllerActionDescriptor = endpoint.Metadata
                    .OfType<ControllerActionDescriptor>()
                    .SingleOrDefault();

                if (controllerActionDescriptor != null)
                {
                    attributes.AddRange(GetAttributes(controllerActionDescriptor.MethodInfo));

                    if (!attributes.Any())
                        attributes.AddRange(GetAttributes(controllerActionDescriptor.ControllerTypeInfo
                            .UnderlyingSystemType));
                }
            }

            return HandleRequirementAsync(context, requirement, attributes);
        }

        protected abstract Task HandleRequirementAsync(AuthorizationHandlerContext context, TRequirement requirement,
            IEnumerable<TAttribute> attributes);

        private static IEnumerable<TAttribute> GetAttributes(MemberInfo memberInfo)
        {
            return memberInfo.GetCustomAttributes(typeof(TAttribute), false).Cast<TAttribute>();
        }
    }
}
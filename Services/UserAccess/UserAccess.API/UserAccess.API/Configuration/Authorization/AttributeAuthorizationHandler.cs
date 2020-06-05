using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UserAccess.API.Configuration.Authorization
{
    public abstract class AttributeAuthorizationHandler<TRequirement, TAttribute> : AuthorizationHandler<TRequirement> where TRequirement : IAuthorizationRequirement where TAttribute : Attribute
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        protected AttributeAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TRequirement requirement)
        {

            //var httpContext = httpContextAccessor.HttpContext;
            //var query = httpContext.Request.Query;
            //var token = httpContext.GetTokenAsync("access_token").Result;
            //if (query.ContainsKey("key") && query.ContainsKey("app"))
            //{
            //    // ...
            //}

            if (context.Resource is Endpoint endpoint)
            {




                var rs = endpoint.Metadata.OfType<HasPermissionAttribute>().ToList();
                if (endpoint.Metadata.OfType<IFilterMetadata>().Any())
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
            }


            //var attributes = endpoint.GetOrderedMetadata<TAttribute>();

            var attributes = new List<TAttribute>();

            if ((context.Resource as AuthorizationFilterContext)?.ActionDescriptor is ControllerActionDescriptor action)
            {
                attributes.AddRange(GetAttributes(action.MethodInfo));

                if (!attributes.Any())
                {
                    attributes.AddRange(GetAttributes(action.ControllerTypeInfo.UnderlyingSystemType));
                }
            }

            return HandleRequirementAsync(context, requirement, attributes);
        }

        protected abstract Task HandleRequirementAsync(AuthorizationHandlerContext context, TRequirement requirement, IEnumerable<TAttribute> attributes);

        private static IEnumerable<TAttribute> GetAttributes(MemberInfo memberInfo)
        {
            return memberInfo.GetCustomAttributes(typeof(TAttribute), false).Cast<TAttribute>();
        }
    }
}

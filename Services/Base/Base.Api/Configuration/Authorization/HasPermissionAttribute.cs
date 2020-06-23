using System;
using Microsoft.AspNetCore.Authorization;

namespace Base.Api.Configuration.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public static string HasPermissionPolicyName = "HasPermission";

        public HasPermissionAttribute(string name) : base(HasPermissionPolicyName)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
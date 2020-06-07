using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace UserAccess.API.Configuration.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        internal static string HasPermissionPolicyName = "HasPermission";
        public string Name { get; }

        public HasPermissionAttribute(string name) : base(HasPermissionPolicyName)
        {
            Name = name;
        }
    }
}

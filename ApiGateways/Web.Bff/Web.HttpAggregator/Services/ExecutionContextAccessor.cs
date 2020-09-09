using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Web.HttpAggregator.Services
{
    public class ExecutionContextAccessor : IExecutionContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExecutionContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<string> GetTokenAsync()
        {
            var token = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
                
            return token;
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
                    return Guid.Parse(_httpContextAccessor.HttpContext.User.Claims.Single(x => x.Type == "sub").Value);

                throw new ApplicationException("User context is not available");
            }
        }

    }
}

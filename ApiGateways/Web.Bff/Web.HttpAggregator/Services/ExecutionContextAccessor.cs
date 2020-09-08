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
    }
}

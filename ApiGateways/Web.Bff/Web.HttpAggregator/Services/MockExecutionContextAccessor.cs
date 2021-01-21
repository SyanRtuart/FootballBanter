using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.HttpAggregator.Services
{
    public class MockExecutionContextAccessor : IExecutionContextAccessor
    {
        private Guid _userId;

        public async Task<string> GetTokenAsync()
        {
            return string.Empty;
        }

        public Guid UserId => Guid.Empty;
    }
}

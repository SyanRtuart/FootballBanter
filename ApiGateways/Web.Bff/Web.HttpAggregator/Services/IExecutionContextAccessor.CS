using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.HttpAggregator.Services
{
    public interface IExecutionContextAccessor
    {
        Task<string> GetTokenAsync();

        Guid UserId { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Application.BuildingBlocks
{
    public interface IExecutionContextAccessor
    {
        Guid UserId { get; }

        Guid CorrelationId { get; }

        bool IsAvailable { get; }

        List<string> UserPermissions { get; }
    }
}

using System;
using System.Collections.Generic;

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
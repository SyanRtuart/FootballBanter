using System;
using System.Collections.Generic;
using Base.Application.BuildingBlocks;

namespace Phrases.IntegrationTests.SeedWork
{
    public class ExecutionContextMock : IExecutionContextAccessor
    {
        public ExecutionContextMock(Guid userId)
        {
            UserId = userId;
        }
        public Guid UserId { get; }
        public Guid CorrelationId { get; }
        public bool IsAvailable { get; }
        public List<string> UserPermissions { get; }
    }
}

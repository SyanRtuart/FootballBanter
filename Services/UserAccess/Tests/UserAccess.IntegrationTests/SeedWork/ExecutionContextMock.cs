using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Application.BuildingBlocks;

namespace UserAccess.IntegrationTests.SeedWork
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

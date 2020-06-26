using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Infrastructure.Outbox;
using UserAccess.Infrastructure.Persistence;

namespace UserAccess.Infrastructure.Configuration.Outbox
{
    public class OutboxAccessor : IOutbox
    {
        private readonly UserAccessContext _userAccessContext;

        public OutboxAccessor(UserAccessContext userAccessContext)
        {
            _userAccessContext = userAccessContext;
        }

        public void Add(OutboxMessage message)
        {
            _userAccessContext.OutboxMessages.Add(message);
        }
    }
}

using Base.Infrastructure.Outbox;
using UserAccess.Infrastructure.Persistence;

namespace UserAccess.Infrastructure.Outbox
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

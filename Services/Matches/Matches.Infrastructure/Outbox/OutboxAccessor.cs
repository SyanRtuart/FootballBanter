using Base.Infrastructure.Outbox;
using Matches.Infrastructure.Persistence;

namespace Matches.Infrastructure.Outbox
{
    public class OutboxAccessor : IOutbox
    {
        private readonly MatchContext _matchContext;

        public OutboxAccessor(MatchContext matchContext)
        {
            _matchContext = matchContext;
        }

        public void Add(OutboxMessage message)
        {
            _matchContext.OutboxMessages.Add(message);
        }
    }
}

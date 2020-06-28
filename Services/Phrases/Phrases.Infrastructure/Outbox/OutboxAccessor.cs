using Base.Infrastructure.Outbox;
using Phrases.Infrastructure.Persistence;

namespace Phrases.Infrastructure.Outbox
{
    public class OutboxAccessor : IOutbox
    {
        private readonly PhraseContext _phraseContext;

        public OutboxAccessor(PhraseContext phraseContext)
        {
            _phraseContext = phraseContext;
        }

        public void Add(OutboxMessage message)
        {
            _phraseContext.OutboxMessages.Add(message);
        }
    }
}

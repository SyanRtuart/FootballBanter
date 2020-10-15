using System;
using Base.Infrastructure.EventBus;

namespace Phrases.IntegrationEvents
{
    public class PhraseCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid PhraseId { get; set; }
        public Guid MatchId { get; set; }
        public Guid TeamId { get; set; }
        public Guid CreatedByUserId { get; set; }
        public string Description { get; set; }
        public bool Positive { get; set; }
        public DateTime UtcDateCreated { get; set; }

        public PhraseCreatedIntegrationEvent(Guid id,
            DateTime occurredOn,
            Guid phraseId,
            Guid matchId,
            Guid teamId,
            Guid createdByUserId,
            string description,
            bool positive,
            DateTime utcDateCreated) : base(id, occurredOn)
        {
            PhraseId = phraseId;
            MatchId = matchId;
            TeamId = teamId;
            CreatedByUserId = createdByUserId;
            Description = description;
            Positive = positive;
            UtcDateCreated = utcDateCreated;
        }
    }
}

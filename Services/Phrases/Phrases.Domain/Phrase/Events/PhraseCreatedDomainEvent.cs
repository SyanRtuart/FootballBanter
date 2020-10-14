using System;
using Base.Domain;

namespace Phrases.Domain.Phrase.Events
{
    public class PhraseCreatedDomainEvent : DomainEventBase
    {
        public PhraseCreatedDomainEvent(Guid phraseId, Guid matchId, Guid teamId, Guid createdByUserId, 
            string description, bool positive, DateTime utcDateCreated)
        {
            PhraseId = phraseId;
            MatchId = matchId;
            TeamId = teamId;
            CreatedByUserId = createdByUserId;
            Description = description;
            Positive = positive;
            UtcDateCreated = utcDateCreated;
        }
        public Guid PhraseId { get; set; }
        public Guid MatchId { get; set; }
        public Guid TeamId { get; set; }
        public Guid CreatedByUserId { get; set; }
        public string Description { get; set; }
        public bool Positive { get; set; }
        public DateTime UtcDateCreated { get; set; }
    }
}

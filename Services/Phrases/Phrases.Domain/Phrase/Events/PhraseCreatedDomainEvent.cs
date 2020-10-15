using System;
using Base.Domain;
using Phrases.Domain.Match;
using Phrases.Domain.Team;
using Phrases.Domain.User;

namespace Phrases.Domain.Phrase.Events
{
    public class PhraseCreatedDomainEvent : DomainEventBase
    {
        public PhraseCreatedDomainEvent(PhraseId phraseId, MatchId matchId, TeamId teamId, UserId createdByUserId, 
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
        public PhraseId PhraseId { get; set; }
        public MatchId MatchId { get; set; }
        public TeamId TeamId { get; set; }
        public UserId CreatedByUserId { get; set; }
        public string Description { get; set; }
        public bool Positive { get; set; }
        public DateTime UtcDateCreated { get; set; }
    }
}

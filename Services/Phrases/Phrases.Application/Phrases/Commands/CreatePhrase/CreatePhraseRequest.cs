using System;

namespace Phrases.Application.Phrases.Commands.CreatePhrase
{
    public class CreatePhraseRequest
    {
        public Guid TeamId { get; set; }

        public Guid MatchId { get; set; }

        public Guid CreatedByUserId { get; set; }

        public string Description { get; set; }

        public bool Positive { get; set; }
    }
}
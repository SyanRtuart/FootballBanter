using System;

namespace Web.HttpAggregator.Models.Phrase
{
    public class CreatePhraseRequest
    {
        public Guid MatchId { get; set; }
        public Guid TeamId { get; set; }
        public string Description { get; set; }
        public bool Positive { get; set; }
    }
}
using System;

namespace Web.HttpAggregator.Models.Phrase
{
    public class PhraseData
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public bool Positive { get; set; }

        public int Score { get; set; }
    }
}
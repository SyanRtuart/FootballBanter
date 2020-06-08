using System;

namespace Phrases.Application.Phrases.Queries.GetPhrasesForMatch
{
    public class PhraseDto
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public bool Positive { get; set; }

        public int Score { get; set; }
    }
}
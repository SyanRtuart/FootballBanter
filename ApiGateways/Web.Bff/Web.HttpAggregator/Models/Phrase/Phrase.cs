using System;

namespace Web.HttpAggregator.Models.Phrase
{
    public class Phrase
    {
        public Phrase(Guid id, string description, bool positive, int score)
        {
            Id = id;
            Description = description;
            Positive = positive;
            Score = score;
        }

        public Guid Id { get; }

        public string Description { get; }

        public bool Positive { get; }

        public int Score { get; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Web.HttpAggregator.Models.Phrase
{
    public class Phrase
    {
        public Phrase(int id, string description, bool positive, int score)
        {
            Id = id;
            Description = description;
            Positive = positive;
            Score = score;
        }

        public int Id { get; private set; }

        public string Description { get; private set; }

        public bool Positive { get; private set; }

        public int Score { get; private set; }

    }
}

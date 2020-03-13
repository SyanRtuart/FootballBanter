using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.HttpAggregator.Models.Phrase
{
    public class Phrase
    {
        public Phrase(int id, string description, bool positive)
        {
            Id = id;
            Description = description;
            Positive = positive;
        }

        public int Id { get; set; }

        public string Description { get; set; }

        public bool Positive { get; set; }
    }
}

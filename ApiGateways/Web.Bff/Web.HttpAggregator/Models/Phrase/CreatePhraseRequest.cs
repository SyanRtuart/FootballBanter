using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.HttpAggregator.Models.Phrase
{
    public class CreatePhraseRequest
    {
        public int MatchId { get; set; }
        public int TeamId { get; set; }
        public string Description { get; set; }
        public bool Positive { get; set; }
    }
}

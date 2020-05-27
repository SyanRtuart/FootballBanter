using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.HttpAggregator.Models.Phrase
{
    public class UpvotePhraseRequest
    {
        public UpvotePhraseRequest(int phraseId)
        {
            PhraseId = phraseId;
        }

        public int PhraseId { get; set; }
    }
}

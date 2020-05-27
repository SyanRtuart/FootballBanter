using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.HttpAggregator.Models.Phrase
{
    public class DownvotePhraseRequest
    {
        public DownvotePhraseRequest(int phraseId)
        {
            PhraseId = phraseId;
        }

        public int PhraseId { get; set; }
    }
}

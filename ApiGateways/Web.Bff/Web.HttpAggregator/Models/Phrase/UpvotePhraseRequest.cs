using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.HttpAggregator.Models.Phrase
{
    public class UpvotePhraseRequest
    {
        public UpvotePhraseRequest(Guid phraseId)
        {
            PhraseId = phraseId;
        }

        public Guid PhraseId { get; set; }
    }
}

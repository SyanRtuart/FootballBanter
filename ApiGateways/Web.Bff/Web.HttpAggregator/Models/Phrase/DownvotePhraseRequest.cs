using System;

namespace Web.HttpAggregator.Models.Phrase
{
    public class DownvotePhraseRequest
    {
        public DownvotePhraseRequest(Guid phraseId)
        {
            PhraseId = phraseId;
        }

        public Guid PhraseId { get; set; }
    }
}
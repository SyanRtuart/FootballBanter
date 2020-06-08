using System;

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
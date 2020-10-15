using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phrases.Application.Configuration.Queries;

namespace Phrases.Application.Phrases.Queries.GetPhrase
{
    public class GetPhraseQuery : QueryBase<PhraseDto>
    {
        public GetPhraseQuery(Guid phraseId)
        {
            PhraseId = phraseId;
        }

        public Guid PhraseId { get; set; }
    }
}

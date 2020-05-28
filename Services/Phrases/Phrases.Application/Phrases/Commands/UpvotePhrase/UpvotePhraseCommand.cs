using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Phrases.Application.Phrases.Commands.UpvotePhrase
{
    public class UpvotePhraseCommand : IRequest
    {
        public UpvotePhraseCommand(Guid phraseId)
        {
            PhraseId = phraseId;
        }

        public Guid PhraseId { get; set; }
    }
}

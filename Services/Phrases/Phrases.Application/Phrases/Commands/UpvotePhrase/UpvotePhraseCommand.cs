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
        public UpvotePhraseCommand(int phraseId)
        {
            PhraseId = phraseId;
        }

        public int PhraseId { get; set; }
    }
}

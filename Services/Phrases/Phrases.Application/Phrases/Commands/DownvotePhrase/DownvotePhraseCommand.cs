using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Phrases.Application.Phrases.Commands.DownvotePhrase
{
    public class DownvotePhraseCommand : IRequest
    {
        public DownvotePhraseCommand(int phraseId)
        {
            PhraseId = phraseId;
        }

        public int PhraseId { get; set; }
    }
}

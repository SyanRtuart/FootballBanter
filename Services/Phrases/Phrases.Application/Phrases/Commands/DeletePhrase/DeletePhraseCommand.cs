using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Phrases.Application.Phrases.Commands.DeletePhrase
{
    public class DeletePhraseCommand : IRequest
    {
        public DeletePhraseCommand(Guid phraseId)
        {
            PhraseId = phraseId;
        }

        public Guid PhraseId { get; set; }
    }
}

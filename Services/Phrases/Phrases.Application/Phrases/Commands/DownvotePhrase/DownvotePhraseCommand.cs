using System;
using MediatR;
using Phrases.Application.Contracts;

namespace Phrases.Application.Phrases.Commands.DownvotePhrase
{
    public class DownvotePhraseCommand : CommandBase<Unit>
    {
        public DownvotePhraseCommand(Guid phraseId)
        {
            PhraseId = phraseId;
        }

        public Guid PhraseId { get; set; }
    }
}
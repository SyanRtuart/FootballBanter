using System;
using MediatR;
using Phrases.Application.Contracts;

namespace Phrases.Application.Phrases.Commands.DeletePhrase
{
    public class DeletePhraseCommand : CommandBase<Unit>
    {
        public DeletePhraseCommand(Guid phraseId, Guid deletedByUserId)
        {
            PhraseId = phraseId;
            DeletedByUserId = deletedByUserId;
        }

        public Guid PhraseId { get; set; }

        public Guid DeletedByUserId { get; set; }
    }
}
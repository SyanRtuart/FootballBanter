using System;
using MediatR;
using Phrases.Application.Contracts;

namespace Phrases.Application.Phrases.Commands.DownvotePhrase
{
    public class DownvotePhraseCommand : CommandBase<Unit>
    {
        public DownvotePhraseCommand(Guid phraseId, Guid votedByUserId)
        {
            PhraseId = phraseId;
            VotedByUserId = votedByUserId;
        }

        public Guid PhraseId { get; set; }

        public Guid VotedByUserId { get; set; }
    }
}
using System;
using MediatR;
using Phrases.Application.Contracts;

namespace Phrases.Application.Phrases.Commands.UpvotePhrase
{
    public class UpvotePhraseCommand : CommandBase<Unit>
    {
        public UpvotePhraseCommand(Guid phraseId, Guid votedByUserId)
        {
            PhraseId = phraseId;
            VotedByUserId = votedByUserId;
        }

        public Guid PhraseId { get; set; }

        public Guid VotedByUserId { get; set; }
    }
}
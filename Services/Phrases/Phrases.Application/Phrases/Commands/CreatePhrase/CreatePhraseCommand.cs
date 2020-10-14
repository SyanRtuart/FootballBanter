using System;
using MediatR;
using Phrases.Application.Contracts;

namespace Phrases.Application.Phrases.Commands.CreatePhrase
{
    public class CreatePhraseCommand : CommandBase<Guid>
    {
        public CreatePhraseCommand(Guid matchId, Guid teamId, Guid createdByUserId, string description, bool positive)
        {
            TeamId = teamId;
            MatchId = matchId;
            CreatedByUserId = createdByUserId;
            Description = description;
            Positive = positive;
        }

        public Guid TeamId { get; set; }

        public Guid MatchId { get; set; }

        public Guid CreatedByUserId { get; set; }

        public string Description { get; set; }

        public bool Positive { get; set; }
    }
}
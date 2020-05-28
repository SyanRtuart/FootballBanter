using System;
using MediatR;

namespace Phrases.Application.Phrases.Commands.CreatePhrase
{
    public class CreatePhraseCommand : IRequest
    {
        public CreatePhraseCommand(Guid matchId, Guid teamId, string description, bool positive)
        {
            TeamId = teamId;
            MatchId = matchId;
            Description = description;
            Positive = positive;
        }

        public Guid TeamId { get; set; }

        public Guid MatchId { get; set; }

        public string Description { get; set; }

        public bool Positive { get; set; }
    }
}
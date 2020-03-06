using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Phrases.Application.Phrases.Commands.CreatePhrase
{
    public class CreatePhraseCommand : IRequest
    {
        public CreatePhraseCommand(string description, int teamId, int matchId, bool positive)
        {
            Description = description;
            TeamId = teamId;
            MatchId = matchId;
            Positive = positive;
        }

        public string Description { get; set; }

        public int TeamId { get; set; }

        public int MatchId { get; set; }

        public bool Positive { get; set; }
    }
}

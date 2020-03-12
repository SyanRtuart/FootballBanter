using MediatR;

namespace Phrases.Application.Phrases.Commands.CreatePhrase
{
    public class CreatePhraseCommand : IRequest
    {
        public CreatePhraseCommand(int matchId, int teamId, string description, bool positive)
        {
            TeamId = teamId;
            MatchId = matchId;
            Description = description;
            Positive = positive;
        }

        public int TeamId { get; set; }

        public int MatchId { get; set; }

        public string Description { get; set; }

        public bool Positive { get; set; }
    }
}
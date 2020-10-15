using System;
using System.Threading;
using System.Threading.Tasks;
using Phrases.Application.Configuration.Commands;
using Phrases.Domain.Match;
using Phrases.Domain.Phrase;
using Phrases.Domain.Team;
using Phrases.Domain.User;

namespace Phrases.Application.Phrases.Commands.CreatePhrase
{
    public class CreatePhraseCommandHandler : ICommandHandler<CreatePhraseCommand, Guid>
    {
        private readonly IPhraseRepository _phraseRepository;

        public CreatePhraseCommandHandler(IPhraseRepository phraseRepository)
        {
            _phraseRepository = phraseRepository;
        }

        public async Task<Guid> Handle(CreatePhraseCommand request, CancellationToken cancellationToken)
        {
            var phrase = Phrase.Create(new MatchId(request.MatchId),new TeamId(request.TeamId), new UserId(request.CreatedByUserId), 
                request.Description, request.Positive);

            await _phraseRepository.AddAsync(phrase);

            return phrase.Id.Value;
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Phrases.Domain.Phrase;

namespace Phrases.Application.Phrases.Commands.CreatePhrase
{
    public class CreatePhraseCommandHandler : IRequestHandler<CreatePhraseCommand, Guid>
    {
        private readonly IPhraseRepository _phraseRepository;

        public CreatePhraseCommandHandler(IPhraseRepository phraseRepository)
        {
            _phraseRepository = phraseRepository;
        }

        public async Task<Guid> Handle(CreatePhraseCommand request, CancellationToken cancellationToken)
        {
            var phrase = Phrase.Create(request.MatchId, request.TeamId, request.Description, request.Positive);

            _phraseRepository.Add(phrase);

            await _phraseRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return phrase.Id;
        }
    }
}
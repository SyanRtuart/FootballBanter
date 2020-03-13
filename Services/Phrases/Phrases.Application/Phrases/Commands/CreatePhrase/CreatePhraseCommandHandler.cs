using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Phrases.Domain.Phrase;

namespace Phrases.Application.Phrases.Commands.CreatePhrase
{
    public class CreatePhraseCommandHandler : IRequestHandler<CreatePhraseCommand, Unit>
    {
        private readonly IPhraseRepository _phraseRepository;

        public CreatePhraseCommandHandler(IPhraseRepository phraseRepository)
        {
            _phraseRepository = phraseRepository;
        }

        public async Task<Unit> Handle(CreatePhraseCommand request, CancellationToken cancellationToken)
        {
            var phrase = new Phrase(request.TeamId, request.MatchId, request.Description, request.Positive);

            _phraseRepository.Add(phrase);

            await _phraseRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
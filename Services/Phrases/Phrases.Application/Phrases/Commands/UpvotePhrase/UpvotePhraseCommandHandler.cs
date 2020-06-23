using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Phrases.Domain.Phrase;

namespace Phrases.Application.Phrases.Commands.UpvotePhrase
{
    public class UpvotePhraseCommandHandler : IRequestHandler<UpvotePhraseCommand, Unit>
    {
        private readonly IPhraseRepository _phraseRepository;

        public UpvotePhraseCommandHandler(IPhraseRepository phraseRepository)
        {
            _phraseRepository = phraseRepository;
        }

        public async Task<Unit> Handle(UpvotePhraseCommand request, CancellationToken cancellationToken)
        {
            var phrase = await _phraseRepository.GetAsync(request.PhraseId);

            phrase.Upvote();

            await _phraseRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
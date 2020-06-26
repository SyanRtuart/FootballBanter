using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Phrases.Domain.Phrase;

namespace Phrases.Application.Phrases.Commands.DownvotePhrase
{
    public class DownvotePhraseCommandHandler : IRequestHandler<DownvotePhraseCommand, Unit>
    {
        private readonly IPhraseRepository _phraseRepository;

        public DownvotePhraseCommandHandler(IPhraseRepository phraseRepository)
        {
            _phraseRepository = phraseRepository;
        }

        public async Task<Unit> Handle(DownvotePhraseCommand request, CancellationToken cancellationToken)
        {
            var phrase = await _phraseRepository.GetAsync(request.PhraseId);

            phrase.Downvote();

            return Unit.Value;
        }
    }
}
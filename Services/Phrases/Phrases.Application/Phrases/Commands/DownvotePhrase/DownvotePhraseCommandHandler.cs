using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Phrases.Application.Configuration.Commands;
using Phrases.Domain.Phrase;

namespace Phrases.Application.Phrases.Commands.DownvotePhrase
{
    public class DownvotePhraseCommandHandler : ICommandHandler<DownvotePhraseCommand, Unit>
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
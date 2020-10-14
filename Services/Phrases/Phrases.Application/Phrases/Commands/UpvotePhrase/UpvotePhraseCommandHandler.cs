using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Phrases.Application.Configuration.Commands;
using Phrases.Domain.Phrase;

namespace Phrases.Application.Phrases.Commands.UpvotePhrase
{
    public class UpvotePhraseCommandHandler : ICommandHandler<UpvotePhraseCommand, Unit>
    {
        private readonly IPhraseRepository _phraseRepository;

        public UpvotePhraseCommandHandler(IPhraseRepository phraseRepository)
        {
            _phraseRepository = phraseRepository;
        }

        public async Task<Unit> Handle(UpvotePhraseCommand request, CancellationToken cancellationToken)
        {
            var phrase = await _phraseRepository.GetAsync(request.PhraseId);
            
            phrase.Upvote(request.VotedByUserId);

            return Unit.Value;
        }
    }
}
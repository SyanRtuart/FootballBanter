using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Phrases.Application.Configuration.Commands;
using Phrases.Domain.Phrase;
using Phrases.Domain.User;

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
            var phrase = await _phraseRepository.GetAsync(new PhraseId(request.PhraseId));
            
            phrase.Upvote(new UserId(request.VotedByUserId));

            return Unit.Value;
        }
    }
}
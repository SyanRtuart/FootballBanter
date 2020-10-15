using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Phrases.Application.Configuration.Commands;
using Phrases.Domain.Phrase;
using Phrases.Domain.User;

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
            var phrase = await _phraseRepository.GetAsync(new PhraseId(request.PhraseId));

            phrase.Downvote(new UserId(request.VotedByUserId));

            return Unit.Value;
        }
    }
}
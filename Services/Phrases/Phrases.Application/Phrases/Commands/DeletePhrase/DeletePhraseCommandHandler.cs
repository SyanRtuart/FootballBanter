using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Phrases.Application.Configuration.Commands;
using Phrases.Domain.Phrase;
using Phrases.Domain.User;

namespace Phrases.Application.Phrases.Commands.DeletePhrase
{
    public class DeletePhraseCommandHandler : ICommandHandler<DeletePhraseCommand, Unit>
    {
        private readonly IPhraseRepository _phraseRepository;

        public DeletePhraseCommandHandler(IPhraseRepository phraseRepository)
        {
            _phraseRepository = phraseRepository;
        }

        public async Task<Unit> Handle(DeletePhraseCommand request, CancellationToken cancellationToken)
        {
            var phrase = await _phraseRepository.GetAsync(new PhraseId(request.PhraseId));

            phrase.Delete(new UserId(request.DeletedByUserId));

            return Unit.Value;
        }
    }
}
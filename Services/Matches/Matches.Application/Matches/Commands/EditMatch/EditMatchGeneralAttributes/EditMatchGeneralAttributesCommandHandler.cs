using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Matches.Application.Configuration.Commands;
using Matches.Domain.Match;
using MediatR;

namespace Matches.Application.Matches.Commands.EditMatch.EditMatchGeneralAttributes
{
    public class EditMatchGeneralAttributesCommandHandler : ICommandHandler<EditMatchGeneralAttributesCommand, Unit>
    {
        private readonly IMatchRepository _matchRepository;

        public EditMatchGeneralAttributesCommandHandler(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public async Task<Unit> Handle(EditMatchGeneralAttributesCommand request, CancellationToken cancellationToken)
        {
            var match = await _matchRepository.GetAsync(request.MatchId);

            match.EditGeneralAttributes(request.Name, request.UtcDate, request.Score, request.Season, request.Status);

            await _matchRepository.CommitAsync();

            return Unit.Value;
        }
    }
}

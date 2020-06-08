using System;
using System.Collections.Generic;
using MediatR;

namespace Phrases.Application.Phrases.Queries.GetPhrasesForMatch
{
    public class GetPhrasesForMatchQuery : IRequest<List<PhraseDto>>
    {
        public GetPhrasesForMatchQuery(Guid matchId)
        {
            MatchId = matchId;
        }

        public Guid MatchId { get; }
    }
}
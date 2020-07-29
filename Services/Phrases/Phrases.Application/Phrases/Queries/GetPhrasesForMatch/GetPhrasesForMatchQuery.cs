using System;
using System.Collections.Generic;
using MediatR;
using Phrases.Application.Configuration.Queries;

namespace Phrases.Application.Phrases.Queries.GetPhrasesForMatch
{
    public class GetPhrasesForMatchQuery : QueryBase<List<PhraseDto>>
    {
        public GetPhrasesForMatchQuery(Guid matchId)
        {
            MatchId = matchId;
        }

        public Guid MatchId { get; }
    }
}
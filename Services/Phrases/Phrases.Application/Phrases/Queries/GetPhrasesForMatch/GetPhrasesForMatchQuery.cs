using System.Collections.Generic;
using MediatR;

namespace Phrases.Application.Phrases.Queries.GetPhrasesForMatch
{
    public class GetPhrasesForMatchQuery : IRequest<List<PhraseDto>>
    {
        public GetPhrasesForMatchQuery(int matchId)
        {
            MatchId = matchId;
        }

        public int MatchId { get;}
    }
}

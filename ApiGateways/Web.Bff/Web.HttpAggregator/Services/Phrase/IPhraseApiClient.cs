using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.HttpAggregator.Models.Phrase;
using Web.HttpAggregator.Models.Phrase.Member;

namespace Web.HttpAggregator.Services.Phrase
{
    public interface IPhraseApiClient
    {
        Task<List<PhraseData>> GetPhrasesAsync(Guid matchId);

        Task<Guid> CreatePhrase(CreatePhraseRequest request);

        Task UpvotePhrase(UpvotePhraseRequest request);

        Task DownvotePhrase(DownvotePhraseRequest request);

        Task<MemberData> GetMember(GetMemberRequest request);

        Task UpdateMemberGeneralAttributes(EditMemberGeneralAttributesRequest request);
    }
}
using System;

namespace Web.HttpAggregator.Models.Phrase.Member
{
    public class GetMemberRequest
    {
        public GetMemberRequest(Guid memberId)
        {
            MemberId = memberId;
        }

        public Guid MemberId { get; set; }
    }
}

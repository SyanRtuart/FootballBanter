using System;

namespace Web.HttpAggregator.Models.Phrase.Member
{
    public class UpdateMemberGeneralAttributesRequest
    {
        public Guid MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Picture { get; set; }
    }
}

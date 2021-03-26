using System;


namespace Web.HttpAggregator.Models.Phrase.Member
{
    public class EditMemberGeneralAttributesRequest
    {
        public EditMemberGeneralAttributesRequest(Guid memberId, string firstName, string lastName, byte[] picture)
        {
            MemberId = memberId;
            FirstName = firstName;
            LastName = lastName;
            Picture = picture;
        }

        public Guid MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Picture { get; set; }
    }
}

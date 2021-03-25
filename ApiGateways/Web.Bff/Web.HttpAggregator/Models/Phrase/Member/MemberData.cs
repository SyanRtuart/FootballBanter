namespace Web.HttpAggregator.Models.Phrase.Member
{
    public class MemberData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int BanterScore { get; set; }
        public int CommentScore { get; set; }
        public byte[] Picture { get; set; }
    }
}

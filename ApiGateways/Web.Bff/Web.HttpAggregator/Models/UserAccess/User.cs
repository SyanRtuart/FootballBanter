using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.HttpAggregator.Models.UserAccess
{
    public class User
    {
        public User(string firstName, string lastName, string email, string login, int banterScore, int commentScore, byte[] picture)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Login = login;
            BanterScore = banterScore;
            CommentScore = commentScore;
            Picture = picture;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public int BanterScore { get; set; }
        public int CommentScore { get; set; }
        public byte[] Picture { get; set; }
    }
}

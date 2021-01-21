namespace Web.HttpAggregator.Models.UserAccess
{
    public class User
    {
        public User(string firstName, string lastName, string email, string login)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Login = login;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
    }
}

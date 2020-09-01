using System.Collections.Generic;
using Base.Domain.SeedWork;

namespace UserAccess.Domain.Users
{
    public class User : Entity, IAggregateRoot
    {
        private string _email;

        private string _firstName;

        private bool _isActive;

        private string _lastName;
        
        private string _login;

        private string _name;

        private string _password;

        private Scores _scores;

        private List<UserRole> _roles;

        private byte[] _picture;

        private User(string email, string firstName, string lastName, string login, string password)
        {
            _email = email;
            _firstName = firstName;
            _lastName = lastName;
            _login = login;
            _password = password;
        }

        public static User CreateNew(string email, string firstName, string lastName, string login, string password)
        {
            return new User(email, firstName, lastName, login, password);
        }

        private User()
        {
            // Only for EF.
        }
    }
}
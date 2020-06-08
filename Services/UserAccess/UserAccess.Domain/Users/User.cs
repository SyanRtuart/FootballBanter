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

        private List<UserRole> _roles;

        private User()
        {
            // Only for EF.
        }
    }
}
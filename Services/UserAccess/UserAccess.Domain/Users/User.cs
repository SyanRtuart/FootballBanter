using System;
using System.Collections.Generic;
using Base.Domain.SeedWork;
using UserAccess.Domain.UserRegistrations.Events;

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

        public void AddPicture(byte[] picture)
        {
            _picture = picture;
        }

        internal static User CreateFromUserRegistration(Guid id, string email, string firstName, string lastName, string login, string password)
        {
            return new User(id, email, firstName, lastName, login, password);
        }

        private User(Guid id, string email, string firstName, string lastName, string login, string password)
        {
            Id = id;
            _email = email;
            _firstName = firstName;
            _lastName = lastName;
            _login = login;
            _password = password;

            AddDomainEvent(new UserCreatedDomainEvent(Id));
        }

        private User()
        {
            // Only for EF.
        }
    }
}
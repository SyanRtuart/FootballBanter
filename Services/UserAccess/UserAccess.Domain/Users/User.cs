using System;
using System.Collections.Generic;
using Base.Domain.SeedWork;
using UserAccess.Domain.UserRegistrations;
using UserAccess.Domain.UserRegistrations.Events;

namespace UserAccess.Domain.Users
{
    public class User : Entity, IAggregateRoot
    {
        public UserId Id { get; }

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

            //TODO Add domain event
        }

        internal static User CreateFromUserRegistration(UserRegistrationId userRegistrationId, string email, string firstName, string lastName, string login, string password)
        {
            return new User(userRegistrationId, email, firstName, lastName, login, password);
        }

        private User(UserRegistrationId userRegistrationId, string email, string firstName, string lastName, string login, string password)
        {
            Id = new UserId(userRegistrationId.Value);
            _email = email;
            _firstName = firstName;
            _lastName = lastName;
            _login = login;
            _password = password;

            _roles = new List<UserRole>
            {
                UserRole.Member
            };

            AddDomainEvent(new UserCreatedDomainEvent(Id));
        }

        private User()
        {
            // Only for EF.
        }
    }
}
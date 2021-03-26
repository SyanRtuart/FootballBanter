using System;
using System.Collections.Generic;
using Base.Domain.SeedWork;
using UserAccess.Domain.UserRegistrations;
using UserAccess.Domain.UserRegistrations.Events;
using UserAccess.Domain.Users.Events;
using UserAccess.Domain.Users.Rules;

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

        private List<UserRole> _roles;

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

        public void EditGeneralAttributes(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;

            AddDomainEvent(new UserGeneralAttributesEditedDomainEvent(firstName, lastName));
        }

        public void ChangePassword(string oldPassword, string newPassword)
        {
            CheckRule(new PasswordMustMatchRule(_password, oldPassword));

            _password = PasswordManager.HashPassword(newPassword);

            AddDomainEvent(new UserChangedPasswordDomainEvent(Id));
        }

        private User()
        {
            // Only for EF.
        }
    }
}
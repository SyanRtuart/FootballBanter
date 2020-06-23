using System;
using Base.Domain.SeedWork;
using UserAccess.Domain.UserRegistrations.Events;
using UserAccess.Domain.UserRegistrations.Rules;

namespace UserAccess.Domain.UserRegistrations
{
    public class UserRegistration : Entity, IAggregateRoot
    {
        private DateTime? _confirmedDate;

        private string _email;

        private string _firstName;

        private string _lastName;
        private string _login;

        private string _name;

        private string _password;

        private DateTime _registerDate;

        private UserRegistrationStatus _status;

        private UserRegistration()
        {
            // Only EF.
        }

        private UserRegistration(
            string login,
            string password,
            string email,
            string firstName,
            string lastName,
            IUsersCounter usersCounter)
        {
            this.CheckRule(new UserLoginMustBeUniqueRule(usersCounter, login));

            _login = login;
            _password = password;
            _email = email;
            _firstName = firstName;
            _lastName = lastName;
            _name = $"{firstName} {lastName}";
            _registerDate = DateTime.UtcNow;
            _status = UserRegistrationStatus.WaitingForConfirmation;

            this.AddDomainEvent(new NewUserRegisteredDomainEvent(Id, _login, _email, _firstName, _lastName, _name, _registerDate));
        }

        public static UserRegistration RegisterNewUser(
            string login,
            string password,
            string email,
            string firstName,
            string lastName,
            IUsersCounter usersCounter)
        {
            return new UserRegistration(login, password, email, firstName, lastName, usersCounter);
        }

        public void Confirm()
        {
            this.CheckRule(new UserRegistrationCannotBeConfirmedMoreThanOnceRule(_status));
            this.CheckRule(new UserRegistrationCannotBeConfirmedAfterExpirationRule(_status));

            _status = UserRegistrationStatus.Confirmed;
            _confirmedDate = DateTime.UtcNow;

            this.AddDomainEvent(new UserRegistrationConfirmedDomainEvent(Id));
        }
    }
}
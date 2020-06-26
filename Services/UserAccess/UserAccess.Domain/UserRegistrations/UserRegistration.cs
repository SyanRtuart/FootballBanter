﻿using System;
using Base.Domain.SeedWork;
using UserAccess.Domain.UserRegistrations.Events;
using UserAccess.Domain.UserRegistrations.Rules;

namespace UserAccess.Domain.UserRegistrations
{
    public class UserRegistration : Entity, IAggregateRoot
    {
        private DateTime? _confirmedDate;

        private readonly string _email;

        private readonly string _firstName;

        private readonly string _lastName;
        private readonly string _login;

        private readonly string _name;

        private string _password;

        private readonly DateTime _registerDate;

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
            CheckRule(new UserLoginMustBeUniqueRule(usersCounter, login));

            _login = login;
            _password = password;
            _email = email;
            _firstName = firstName;
            _lastName = lastName;
            _name = $"{firstName} {lastName}";
            _registerDate = DateTime.UtcNow;
            _status = UserRegistrationStatus.WaitingForConfirmation;

            AddDomainEvent(new NewUserRegisteredDomainEvent(Id, _login, _email, _firstName, _lastName, _name,
                _registerDate));
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
            CheckRule(new UserRegistrationCannotBeConfirmedMoreThanOnceRule(_status));
            CheckRule(new UserRegistrationCannotBeConfirmedAfterExpirationRule(_status));

            _status = UserRegistrationStatus.Confirmed;
            _confirmedDate = DateTime.UtcNow;

            AddDomainEvent(new UserRegistrationConfirmedDomainEvent(Id));
        }
    }
}
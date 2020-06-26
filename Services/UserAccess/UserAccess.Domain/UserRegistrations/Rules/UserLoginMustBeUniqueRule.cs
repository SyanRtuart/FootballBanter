﻿using Base.Domain.SeedWork;

namespace UserAccess.Domain.UserRegistrations.Rules
{
    public class UserLoginMustBeUniqueRule : IBusinessRule
    {
        private readonly string _login;
        private readonly IUsersCounter _usersCounter;

        internal UserLoginMustBeUniqueRule(IUsersCounter usersCounter, string login)
        {
            _usersCounter = usersCounter;
            _login = login;
        }

        public bool IsBroken()
        {
            return _usersCounter.CountUsersWithLogin(_login) > 0;
        }

        public string Message => "User Login must be unique";
    }
}
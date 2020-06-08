﻿using MediatR;

namespace UserAccess.Application.Authentication.Authenticate
{
    public class AuthenticateCommand : IRequest<AuthenticationResult>
    {
        public AuthenticateCommand(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public string Login { get; }

        public string Password { get; }
    }
}
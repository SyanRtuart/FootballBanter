using Base.Domain.SeedWork;

namespace UserAccess.Domain.UserRegistrations.Rules
{
    public class UserEmailMustBeUniqueRule : IBusinessRule
    {
        private readonly string _login;
        private readonly IUsersCounter _usersCounter;

        internal UserEmailMustBeUniqueRule(IUsersCounter usersCounter, string login)
        {
            _usersCounter = usersCounter;
            _login = login;
        }

        public bool IsBroken()
        {
            return _usersCounter.CountUsersWithLogin(_login) > 0;
        }

        public string Message => "An account with this e-mail already exists.";
    }
}
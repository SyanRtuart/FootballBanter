using Base.Domain.SeedWork;

namespace UserAccess.Domain.Users.Rules
{
    public class PasswordMustMatchRule : IBusinessRule
    {
        private readonly string _currentHashedPassword;
        private readonly string _password;

        public PasswordMustMatchRule(string currentHashedPassword, string password)
        {
            _currentHashedPassword = currentHashedPassword;
            _password = password;
        }


        public string Message => "Password doesn't match current password";

        public bool IsBroken()
        {
            return !PasswordManager.VerifyHashedPassword(_currentHashedPassword, _password);
        }
    }
}

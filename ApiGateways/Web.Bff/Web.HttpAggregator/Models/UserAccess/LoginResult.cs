namespace Web.HttpAggregator.Models.UserAccess
{
    public class LoginResult
    {
        public LoginResult(string accessToken, int expiresIn, string tokenType, string refreshToken, string scope)
        {
            AccessToken = accessToken;
            ExpiresIn = expiresIn;
            TokenType = tokenType;
            RefreshToken = refreshToken;
            Scope = scope;
        }

        public string AccessToken { get; }

        public int ExpiresIn { get; }

        public string TokenType { get; }

        public string RefreshToken { get; }

        public string Scope { get; }
    }
}
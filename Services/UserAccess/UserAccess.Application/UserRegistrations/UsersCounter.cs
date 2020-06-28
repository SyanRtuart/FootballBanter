using Base.Infrastructure;
using Dapper;
using UserAccess.Domain.UserRegistrations;

namespace UserAccess.Application.UserRegistrations
{
    public class UsersCounter : IUsersCounter
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public UsersCounter(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public int CountUsersWithLogin(string login)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT " +
                               "COUNT(*) " +
                               "FROM [Users].UserRegistrations AS [Users]" +
                               "WHERE [Users].[Login] = @Login";
            return connection.QuerySingle<int>(sql,
                new
                {
                    login
                });
        }
    }
}
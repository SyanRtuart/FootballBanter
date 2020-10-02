using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Base.Infrastructure;
using Dapper;
using UserAccess.Application.Configuration.Queries;

namespace UserAccess.Application.Users.Queries.GetUser
{
    public class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetUserQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = @"SELECT 
                                  Id,
                                  FirstName, 
                                  LastName, 
                                  Email, 
                                  Login, 
                                  BanterScore, 
                                  CommentScore, 
                                  Picture 
                                FROM 
                                  Users.Users 
                                WHERE 
                                  (Id = @userId)";

            var user = await connection.QueryAsync<UserDto>(sql, new {request.UserId});

            return user.First();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Base.Infrastructure;
using Dapper;
using UserAccess.Application.Configuration.Queries;

namespace UserAccess.Application.Users.Queries.GetUserByEmail
{
    public class GetUserByEmailQueryHandler : IQueryHandler<GetUserByEmailQuery, UserDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetUserByEmailQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<UserDto> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = @"SELECT 
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
                                  (Email = @Email)";

            var user = await connection.QueryAsync<UserDto>(sql, new {request.Email});

            return user.SingleOrDefault();
        }
    }
}

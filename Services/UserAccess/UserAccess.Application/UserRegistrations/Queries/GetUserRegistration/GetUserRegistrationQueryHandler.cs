using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Base.Infrastructure;
using Dapper;
using UserAccess.Application.Configuration.Queries;

namespace UserAccess.Application.UserRegistrations.Queries.GetUserRegistration
{
    public class GetUserRegistrationQueryHandler : IQueryHandler<GetUserRegistrationQuery, UserDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetUserRegistrationQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<UserDto> Handle(GetUserRegistrationQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = @"SELECT
                                   Id,
                                   Email,
                                   FirstName,
                                   LastName,
                                   Login,
                                   Name,
                                   StatusCode 
                                FROM
                                   Users.UserRegistrations 
                                WHERE
                                   (Id = @UserRegistrationId)";

            var userRegistration = await connection.QueryAsync<UserDto>(sql, new {request.UserRegistrationId});

            return userRegistration.SingleOrDefault();
        }
    }
}

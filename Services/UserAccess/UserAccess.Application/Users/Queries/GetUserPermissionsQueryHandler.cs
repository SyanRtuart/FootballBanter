﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Base.Infrastructure;
using Dapper;
using MediatR;

namespace UserAccess.Application.Users.Queries
{
    public class GetUserPermissionsQueryHandler : IRequestHandler<GetUserPermissionsQuery, List<UserPermissionDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetUserPermissionsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<UserPermissionDto>> Handle(GetUserPermissionsQuery request,
            CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT DISTINCT " +
                               "UserRole.UserId, " +
                               "RolesToPermission.PermissionCode AS [Code] " +
                               "FROM Users.UserRoles AS UserRole INNER JOIN " +
                               "Users.RolesToPermissions AS RolesToPermission ON UserRole.RoleCode = RolesToPermission.RoleCode " +
                               "WHERE UserRole.UserId = @UserId";

            var permissions = await connection.QueryAsync<UserPermissionDto>(sql, new {request.UserId});
            
            return permissions.AsList();
        }
    }
}
using System;
using System.Collections.Generic;
using MediatR;
using UserAccess.Application.Contracts;

namespace UserAccess.Application.Users.Queries.GetUserPermissions
{
    public class GetUserPermissionsQuery : QueryBase<List<UserPermissionDto>>
    {
        public GetUserPermissionsQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}
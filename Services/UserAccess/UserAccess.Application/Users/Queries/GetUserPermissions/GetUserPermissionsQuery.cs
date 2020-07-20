using System;
using System.Collections.Generic;
using MediatR;

namespace UserAccess.Application.Users.Queries.GetUserPermissions
{
    public class GetUserPermissionsQuery : IRequest<List<UserPermissionDto>>
    {
        public GetUserPermissionsQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}
using System;
using System.Collections.Generic;
using MediatR;

namespace UserAccess.Application.Users.Queries
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
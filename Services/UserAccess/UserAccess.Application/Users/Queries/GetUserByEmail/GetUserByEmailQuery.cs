using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAccess.Application.Contracts;

namespace UserAccess.Application.Users.Queries.GetUserByEmail
{
    public class GetUserByEmailQuery : QueryBase<UserDto>
    {
        public GetUserByEmailQuery(string email)
        {
            Email = email;
        }

        public string Email { get; set; }
    }
}

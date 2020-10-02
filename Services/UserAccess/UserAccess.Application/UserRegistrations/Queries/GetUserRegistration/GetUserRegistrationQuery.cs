using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAccess.Application.Contracts;

namespace UserAccess.Application.UserRegistrations.Queries.GetUserRegistration
{
    public class GetUserRegistrationQuery : QueryBase<UserDto>   
    {
        public GetUserRegistrationQuery(Guid userRegistrationId)
        {
            UserRegistrationId = userRegistrationId;
        }

        public Guid UserRegistrationId { get; set; }
    }
}

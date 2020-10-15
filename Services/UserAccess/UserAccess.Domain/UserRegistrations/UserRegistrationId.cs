using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Domain;

namespace UserAccess.Domain.UserRegistrations
{
    public class UserRegistrationId : TypedIdValueBase
    {
        public UserRegistrationId(Guid value) : base(value)
        {
        }
    }
}

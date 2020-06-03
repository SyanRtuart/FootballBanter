using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAccess.Domain.Exceptions
{
    public class UserAccessDomainException : Exception
    {
        public UserAccessDomainException()
        {
        }

        public UserAccessDomainException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

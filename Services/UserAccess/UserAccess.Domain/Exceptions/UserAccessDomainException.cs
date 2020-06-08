using System;

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
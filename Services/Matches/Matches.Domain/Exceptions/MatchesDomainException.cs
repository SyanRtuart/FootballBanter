using System;

namespace Matches.Domain.Exceptions
{
    public class MatchesDomainException : Exception
    {
        public MatchesDomainException()
        {
        }

        public MatchesDomainException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
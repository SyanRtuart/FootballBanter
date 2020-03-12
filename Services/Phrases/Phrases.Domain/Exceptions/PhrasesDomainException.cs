using System;


namespace Phrases.Domain.Exceptions
{
    public class PhrasesDomainException : Exception
    {
        public PhrasesDomainException()
        {
            
        }

        public PhrasesDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}

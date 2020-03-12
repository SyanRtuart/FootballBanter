using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phrases.Domain.Exceptions
{
    public class PhraseDomainException : Exception
    {
        public PhraseDomainException()
        {
            
        }

        public PhraseDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}

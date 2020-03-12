using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Matches.Domain.Exceptions
{
     public class MatchesDomainException : Exception
     {
         public MatchesDomainException()
         {

         }

         public MatchesDomainException(string message, Exception innerException)
             : base(message, innerException)
         { }
     }
}

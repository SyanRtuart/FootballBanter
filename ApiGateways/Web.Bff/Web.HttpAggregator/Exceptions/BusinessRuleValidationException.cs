using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.HttpAggregator.Exceptions
{
    public class BusinessRuleValidationException : Exception
    {
        public BusinessRuleValidationException(string details) : base (details)
        {
        }
    }
}

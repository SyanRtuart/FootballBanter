using System;


namespace Web.HttpAggregator.Exceptions
{
    public class BusinessRuleValidationException : Exception
    {
        public BusinessRuleValidationException(string details) : base (details)
        {
        }
    }
}

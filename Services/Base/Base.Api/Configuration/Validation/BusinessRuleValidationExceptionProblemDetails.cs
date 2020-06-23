using Base.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Base.Api.Configuration.Validation
{
    public class BusinessRuleValidationExceptionProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        public BusinessRuleValidationExceptionProblemDetails(BusinessRuleValidationException exception)
        {
            Title = "Business rule broken";
            Status = StatusCodes.Status409Conflict;
            Detail = exception.Message;
            Type = "https://somedomain/business-rule-validation-error";
        }
    }
}

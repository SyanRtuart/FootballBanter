using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.HttpAggregator.Exceptions;

namespace Web.HttpAggregator.Models
{
    public class BusinessRuleValidationExceptionProblemDetails : ProblemDetails
    {
        public string Problem { get; set; }

        public BusinessRuleValidationExceptionProblemDetails(BusinessRuleValidationException exception)
        {
            Problem = "Business rule broken.";
            Detail = exception.Message;
        }
    }
}

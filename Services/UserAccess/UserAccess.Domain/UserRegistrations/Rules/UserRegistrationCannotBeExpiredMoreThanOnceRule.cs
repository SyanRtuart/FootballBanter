using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Domain.SeedWork;

namespace UserAccess.Domain.UserRegistrations.Rules
{
    public class UserRegistrationCannotBeExpiredMoreThanOnceRule : IBusinessRule
    {
        private readonly UserRegistrationStatus _actualRegistrationStatus;

        internal UserRegistrationCannotBeExpiredMoreThanOnceRule(UserRegistrationStatus actualRegistrationStatus)
        {
            _actualRegistrationStatus = actualRegistrationStatus;
        }

        public bool IsBroken()
        {
            return _actualRegistrationStatus == UserRegistrationStatus.Expired;
        }

        public string Message => "User Registration cannot be expired more than once.";

    }
}

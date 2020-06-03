using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Domain.SeedWork;

namespace UserAccess.Domain.UserRegistrations
{
    public class UserRegistrationStatus : ValueObject
    {
        public static UserRegistrationStatus WaitingForConfirmation => new UserRegistrationStatus(nameof(WaitingForConfirmation));
        public static UserRegistrationStatus Confirmed => new UserRegistrationStatus(nameof(Confirmed));
        public static UserRegistrationStatus Expired => new UserRegistrationStatus(nameof(Expired));
        public string Value { get; }

        public UserRegistrationStatus(string value)
        {
            Value = value;
        }
    }
}

using Base.Domain.SeedWork;

namespace UserAccess.Domain.UserRegistrations
{
    public class UserRegistrationStatus : ValueObject
    {
        private UserRegistrationStatus(string value)
        {
            Value = value;
        }

        public static UserRegistrationStatus WaitingForConfirmation =>
            new UserRegistrationStatus(nameof(WaitingForConfirmation));

        public static UserRegistrationStatus Confirmed => new UserRegistrationStatus(nameof(Confirmed));
        public static UserRegistrationStatus Expired => new UserRegistrationStatus(nameof(Expired));
        public string Value { get; }
    }
}
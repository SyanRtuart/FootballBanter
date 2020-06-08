using Base.Domain.SeedWork;

namespace UserAccess.Domain.Users
{
    public class UserRole : ValueObject
    {
        private UserRole(string value)
        {
            Value = value;
        }

        public static UserRole Member => new UserRole(nameof(Member));
        public string Value { get; }
    }
}
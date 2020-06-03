using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Domain.SeedWork;

namespace UserAccess.Domain.Users
{
    public class UserRole : ValueObject
    {
        public static UserRole Member => new UserRole(nameof(Member));
        public string Value { get; }

        private UserRole(string value)
        {
            this.Value = value;
        }
    }
}

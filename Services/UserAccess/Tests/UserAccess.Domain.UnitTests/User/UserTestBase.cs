using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using UserAccess.Domain.UnitTests.SeedWork;
using UserAccess.Domain.UserRegistrations;

namespace UserAccess.Domain.UnitTests.User
{
    public class UserTestBase : TestBase
    {
        protected class UserTestDataOptions
        {
            internal string FirstName { get; set; }
            internal string LastName { get; set; }
            internal byte[] Picture { get; set; }
        }

        protected class UserTestData
        {
            public UserTestData(Users.User user)
            {
                User = user;
            }

            internal Users.User User { get; set; }
        }

        protected UserTestData CreateUserTestData(UserTestDataOptions options)
        {
            var usersCounter = Substitute.For<IUsersCounter>();

            var userRegistration =
                UserRegistration.RegisterNewUser(
                    "login", "password", "test@email",
                    options.FirstName, options.LastName, usersCounter);

            userRegistration.Confirm();
            var user = userRegistration.CreateUser();

            DomainEventsTestHelper.ClearAllDomainEvents(user);

            return new UserTestData(user);
        }
    }
}

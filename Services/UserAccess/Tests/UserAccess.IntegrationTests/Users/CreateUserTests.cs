using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using UserAccess.Application.UserRegistrations.Commands.ConfirmUserRegistration;
using UserAccess.Application.UserRegistrations.Commands.RegisterNewUser;
using UserAccess.Application.Users.Commands.CreateUser;
using UserAccess.Application.Users.Queries.GetUser;
using UserAccess.IntegrationTests.SeedWork;
using UserAccess.IntegrationTests.UserRegistrations;

namespace UserAccess.IntegrationTests.Users
{
    [TestFixture]
    public class CreateUserTests : TestBase
    {
        [Test]
        public async Task CreateUser_Test()
        {
            var userRegistrationId = await UserAccessModule.ExecuteCommandAsync(new RegisterNewUserCommand(
                UserRegistrationSampleData.Login,
                UserRegistrationSampleData.Password,
                UserRegistrationSampleData.Email,
                UserRegistrationSampleData.FirstName,
                UserRegistrationSampleData.LastName));

            await UserAccessModule.ExecuteCommandAsync(new ConfirmUserRegistrationCommand(userRegistrationId));

            var userId = await UserAccessModule.ExecuteCommandAsync(new CreateUserCommand(
                Guid.NewGuid(),
                userRegistrationId));

            var user = await UserAccessModule.ExecuteQueryAsync(new GetUserQuery(userId));

            Assert.That(user.Login, Is.EqualTo(UserRegistrationSampleData.Login));
        }
    }
}

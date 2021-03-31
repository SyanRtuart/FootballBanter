using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using UserAccess.Application.Authentication;
using UserAccess.Application.UserRegistrations.Commands.ConfirmUserRegistration;
using UserAccess.Application.UserRegistrations.Commands.RegisterNewUser;
using UserAccess.Application.Users.Commands.ChangePassword;
using UserAccess.Application.Users.Commands.CreateUser;
using UserAccess.Application.Users.Commands.EditUserGeneralAttributes;
using UserAccess.Application.Users.Queries.GetUser;
using UserAccess.Domain.UserRegistrations;
using UserAccess.IntegrationTests.SeedWork;
using UserAccess.IntegrationTests.UserRegistrations;

namespace UserAccess.IntegrationTests.Users
{
    [TestFixture]
    public class UserTests : UserTestBase
    {
        [Test]
        public async Task CreateUserCommand_Test()
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
                new UserRegistrationId(userRegistrationId)));

            var user = await UserAccessModule.ExecuteQueryAsync(new GetUserQuery(userId));

            Assert.That(user.Login, Is.EqualTo(UserRegistrationSampleData.Login));
        }

        [Test]
        public async Task UserGeneralAttributesCommand_Test()
        {
            var userId = await CreateUserTestData();

            await UserAccessModule.ExecuteCommandAsync(new EditUserGeneralAttributesCommand(
                userId,
                EditUserSampleData.NewFirstName,
                EditUserSampleData.NewLastName));

            var user = await UserAccessModule.ExecuteQueryAsync(new GetUserQuery(userId));

            Assert.That(user.FirstName, Is.EqualTo(EditUserSampleData.NewFirstName));
            Assert.That(user.LastName, Is.EqualTo(EditUserSampleData.NewLastName));
        }

        [Test]
        public async Task ChangePassword_Test()
        {
            var userId = await CreateUserTestData();

            await UserAccessModule.ExecuteCommandAsync(new ChangePasswordCommand(userId,
                UserRegistrationSampleData.Password, "newPassword123"));


        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAccess.Application.UserRegistrations.Commands.ConfirmUserRegistration;
using UserAccess.Application.UserRegistrations.Commands.RegisterNewUser;
using UserAccess.Application.Users.Commands.CreateUser;
using UserAccess.Application.Users.Queries.GetUser;
using UserAccess.Domain.UserRegistrations;
using UserAccess.IntegrationTests.SeedWork;
using UserAccess.IntegrationTests.UserRegistrations;

namespace UserAccess.IntegrationTests.Users
{
    public class UserTestBase : TestBase
    {
        protected async Task<Guid> CreateUserTestData()
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



            return userId;
        }
    }
}

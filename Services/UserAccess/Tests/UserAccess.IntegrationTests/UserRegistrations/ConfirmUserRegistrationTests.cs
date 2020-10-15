using System.Threading.Tasks;
using NUnit.Framework;
using UserAccess.Application.UserRegistrations.Commands.ConfirmUserRegistration;
using UserAccess.Application.UserRegistrations.Commands.RegisterNewUser;
using UserAccess.Application.UserRegistrations.Queries.GetUserRegistration;
using UserAccess.Application.Users.Commands.CreateUser;
using UserAccess.Domain.UserRegistrations;
using UserAccess.IntegrationTests.SeedWork;

namespace UserAccess.IntegrationTests.UserRegistrations
{
    [TestFixture]
    public class ConfirmUserRegistrationTests : TestBase
    {
        [Test]
        public async Task ConfirmUserRegistration_Test()
        {
            var registrationId = await UserAccessModule.ExecuteCommandAsync(new RegisterNewUserCommand(
                UserRegistrationSampleData.Login,
                UserRegistrationSampleData.Password,
                UserRegistrationSampleData.Email,
                UserRegistrationSampleData.FirstName,
                UserRegistrationSampleData.LastName));

            await UserAccessModule.ExecuteCommandAsync(new ConfirmUserRegistrationCommand(registrationId));

            var userRegistration = await UserAccessModule.ExecuteQueryAsync(new GetUserRegistrationQuery(registrationId));

            Assert.That(userRegistration.StatusCode, Is.EqualTo(UserRegistrationStatus.Confirmed.Value));

            var userRegistrationConfirmedNotification = await GetLastOutboxMessage<UserRegistrationConfirmedNotification>();

            Assert.That(userRegistrationConfirmedNotification.DomainEvent.UserRegistrationId.Value, Is.EqualTo(registrationId));
        }
    }
}

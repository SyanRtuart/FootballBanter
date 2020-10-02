using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Application.Emails;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using UserAccess.Application.UserRegistrations.Commands.SendUserRegistrationConfirmationEmail;
using UserAccess.IntegrationTests.SeedWork;

namespace UserAccess.IntegrationTests.UserRegistrations
{
    [TestFixture]
    public class SendUserRegistrationConfirmationEmailCommandHandlerTests : TestBase
    {
        [Test]
        public async Task SendUserRegistrationConfirmationEmail_Test()
        {
            var userRegistrationId = Guid.NewGuid();

            var command = new SendUserRegistrationConfirmationEmailCommand(Guid.NewGuid(), userRegistrationId, UserRegistrationSampleData.Email);

            var email = new EmailMessage(UserRegistrationSampleData.Email, "FootballBanter - Please confirm your registration",
                "This should be link to confirmation page. For now, please execute HTTP request " +
                $"PATCH http://localhost:5000/userAccess/userRegistrations/{userRegistrationId}/confirm");

            await UserAccessModule.ExecuteCommandAsync(command);

            await EmailSender.Received(Quantity.Exactly(1)).SendEmail(email);
        }
    }
}

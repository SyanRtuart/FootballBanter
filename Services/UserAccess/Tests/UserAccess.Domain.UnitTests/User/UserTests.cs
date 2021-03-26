using NSubstitute;
using NUnit.Framework;
using UserAccess.Domain.UserRegistrations;
using UserAccess.Domain.Users.Events;
using UserAccess.Domain.Users.Rules;

namespace UserAccess.Domain.UnitTests.User
{
    [TestFixture]
    public class UserTests : UserTestBase
    {
        [Test]
        public void CreateUserTest_IsSuccessful()
        {
            var usersCounter = Substitute.For<IUsersCounter>();

            var userRegistration =
                UserRegistration.RegisterNewUser(
                    "login", "password", "test@email",
                    "firstName", "lastName", usersCounter);

            userRegistration.Confirm();
            var user = userRegistration.CreateUser();

            var userCreatedDomainEvent = AssertPublishedDomainEvent<UserCreatedDomainEvent>(user);
            Assert.That(userCreatedDomainEvent.UserId, Is.EqualTo(user.Id));
        }

        [Test]
        public void EditGeneralAttributes_IsSuccessful()
        {
            var user = CreateUserTestData(new UserTestDataOptions
            {
                FirstName = "firstName",
                LastName = "lastName",
            });

            user.User.EditGeneralAttributes("newFirstname", "newLastname");

            var userGeneralAttributesEditedDomainEvent = AssertPublishedDomainEvent<UserGeneralAttributesEditedDomainEvent>(user.User);
            Assert.That(userGeneralAttributesEditedDomainEvent.FirstName, Is.EqualTo("newFirstname"));
            Assert.That(userGeneralAttributesEditedDomainEvent.LastName, Is.EqualTo("newLastname"));
        }

        [Test]
        public void ChangePassword_WhenPasswordMatches_IsSuccessful()
        {
            var user = CreateUserTestData(new UserTestDataOptions
            {
                FirstName = "firstName",
                LastName = "lastName",
                Password = "testPassword123"
            });

            user.User.ChangePassword("testPassword123", "newPassword123");

            var userPasswordChangedDomainEvent = AssertPublishedDomainEvent<UserChangedPasswordDomainEvent>(user.User);
            Assert.That(userPasswordChangedDomainEvent.UserId, Is.EqualTo(user.User.Id));
        }

        [Test]
        public void ChangePassword_WhenPasswordsDoNotMatch_IsNotSuccessful()
        {
            // Arrange
            var user = CreateUserTestData(new UserTestDataOptions
            {
                FirstName = "firstName",
                LastName = "lastName",
                Password = "testPassword123"
            });

            // Assert
            AssertBrokenRule<PasswordMustMatchRule>(() =>
            {
                // Act
                user.User.ChangePassword("notMatchingPassword", "newPassword123");
            });
        }
    }
}

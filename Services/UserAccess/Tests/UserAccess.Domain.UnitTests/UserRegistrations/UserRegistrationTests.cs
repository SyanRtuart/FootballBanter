using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using UserAccess.Domain.UnitTests.SeedWork;
using UserAccess.Domain.UserRegistrations;
using UserAccess.Domain.UserRegistrations.Events;
using UserAccess.Domain.UserRegistrations.Rules;
using UserAccess.Domain.Users.Events;

namespace UserAccess.Domain.UnitTests.UserRegistrations
{
    [TestFixture]
    public class UserRegistrationTests : TestBase
    {
        [Test]
        public void NewUserRegistration_WithUniqueLogin_IsSuccessful()
        {
            var usersCounter = Substitute.For<IUsersCounter>();

            var userRegistration =
                UserRegistration.RegisterNewUser(
                    "login", "password", "test@email",
                    "firstName", "lastName", usersCounter);

            var newUserRegisteredDomainEvent = AssertPublishedDomainEvent<NewUserRegisteredDomainEvent>(userRegistration);
            Assert.That(newUserRegisteredDomainEvent.UserRegistrationId, Is.EqualTo(userRegistration.Id));
        }

        [Test]
        public void NewUserRegistration_WithoutUniqueLogin_BreaksUserLoginMustBeUniqueRule()
        {
            // Arrange
            var usersCounter = Substitute.For<IUsersCounter>();
            usersCounter.CountUsersWithLogin("login").Returns(x => 1);

            // Assert
            AssertBrokenRule<UserEmailMustBeUniqueRule>(() =>
            {
                // Act
                UserRegistration.RegisterNewUser(
                    "login", "password", "test@email",
                    "firstName", "lastName", usersCounter);
            });
        }

        [Test]
        public void UserRegistration_WhenIsConfirmed_CannotBeConfirmedAgain()
        {
            var usersCounter = Substitute.For<IUsersCounter>();

            var registration = UserRegistration.RegisterNewUser(
                "login", "password", "test@email",
                "firstName", "lastName", usersCounter);

            registration.Confirm();

            AssertBrokenRule<UserRegistrationCannotBeConfirmedMoreThanOnceRule>(() =>
            {
                registration.Confirm();
            });
        }

        [Test]
        public void UserRegistration_WhenIsExpired_CannotBeConfirmed()
        {
            var usersCounter = Substitute.For<IUsersCounter>();

            var registration = UserRegistration.RegisterNewUser(
                "login", "password", "test@email",
                "firstName", "lastName", usersCounter);

            registration.Expire();

            AssertBrokenRule<UserRegistrationCannotBeConfirmedAfterExpirationRule>(() =>
            {
                registration.Confirm();
            });
        }

        [Test]
        public void ExpiringUserRegistration_WhenWaitingForConfirmation_IsSuccessful()
        {
            var usersCounter = Substitute.For<IUsersCounter>();

            var registration = UserRegistration.RegisterNewUser(
                "login", "password", "test@email",
                "firstName", "lastName", usersCounter);

            registration.Expire();

            var userRegistrationExpired = AssertPublishedDomainEvent<UserRegistrationExpiredDomainEvent>(registration);

            Assert.That(userRegistrationExpired.UserRegistrationId, Is.EqualTo(registration.Id));
        }

        [Test]
        public void UserRegistration_WhenIsExpired_CannotBeExpiredAgain()
        {
            var usersCounter = Substitute.For<IUsersCounter>();

            var registration = UserRegistration.RegisterNewUser(
                "login", "password", "test@email",
                "firstName", "lastName", usersCounter);

            registration.Expire();

            AssertBrokenRule<UserRegistrationCannotBeExpiredMoreThanOnceRule>(() =>
            {
                registration.Expire();
            });
        }

        [Test]
        public void CreateUser_WhenRegistrationIsConfirmed_IsSuccessful()
        {
            var usersCounter = Substitute.For<IUsersCounter>();

            var registration = UserRegistration.RegisterNewUser(
                "login", "password", "test@email",
                "firstName", "lastName", usersCounter);

            registration.Confirm();

            var user = registration.CreateUser();

            var userCreatedId = AssertPublishedDomainEvent<UserCreatedDomainEvent>(user);

            Assert.That(user.Id, Is.EqualTo(registration.Id));
            Assert.That(userCreatedId.UserId, Is.EqualTo(registration.Id));
        }

        [Test]
        public void UserCreation_WhenRegistrationIsNotConfirmed_IsNotPossible()
        {
            var usersCounter = Substitute.For<IUsersCounter>();

            var registration = UserRegistration.RegisterNewUser(
                "login", "password", "test@email",
                "firstName", "lastName", usersCounter);

            AssertBrokenRule<UserCannotBeCreatedWhenRegistrationIsNotConfirmedRule>(
                () =>
                {
                    registration.CreateUser();
                });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using UserAccess.Domain.UnitTests.SeedWork;
using UserAccess.Domain.UserRegistrations;
using UserAccess.Domain.Users.Events;

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


            //user.EditGeneralAttributes("newFirstname", "newLastname", new byte[1]);

            //var  userGeneralAttributesEditedDomainEvent = AssertPublishedDomainEvent<UserGeneralAttributesEditedDomainEvent>(user);
            //Assert.That(newUserRegisteredDomainEvent.FirstName, Is.EqualTo("newFirstname"));
            //Assert.That(newUserRegisteredDomainEvent.LastName, Is.EqualTo("newLastname"));
            //Assert.That(newUserRegisteredDomainEvent.Picture, Is.EqualTo(new byte[1]));
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
    }
}

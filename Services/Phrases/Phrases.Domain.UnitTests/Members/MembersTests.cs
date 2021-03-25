using System;
using NUnit.Framework;
using Phrases.Domain.Members;
using Phrases.Domain.Members.Events;

namespace Phrases.Domain.UnitTests.Members
{
    [TestFixture]
    public class MembersTests : MembersTestsBase
    {
        [Test]
        public void CreateMember_WhenAllConditionsAllow_IsSuccessful()
        {
            var member = Member.CreateNew(Guid.NewGuid(), "email", "login", "firstName", "lastName", "name");

            var memberCreatedDomainEvent = AssertPublishedDomainEvent<MemberCreatedDomainEvent>(member);
            Assert.That(memberCreatedDomainEvent.MemberId, Is.EqualTo(member.Id));
        }

        [Test]
        public void EditGeneralAttributes_WhenAllConditionsAllow_IsSuccessful()
        {
            var memberTestDataOptions =
                new MemberTestDataOptions(Guid.NewGuid(), "email", "login", "firstName", "lastName", "name", new byte[1]);

            var memberTestData = CreateMemberTestData(memberTestDataOptions);
            
            memberTestData.Member.EditGeneralAttributes("newFirstName", "newLastName", new byte[2]);

            var publishedMemberGeneralAttributesEditedDomainEvent =
                AssertPublishedDomainEvent<MemberGeneralAttributesEditedDomainEvent>(memberTestData.Member);
            Assert.That(publishedMemberGeneralAttributesEditedDomainEvent.FirstName, Is.Not.EqualTo(memberTestDataOptions.FirstName));
            Assert.That(publishedMemberGeneralAttributesEditedDomainEvent.LastName, Is.Not.EqualTo(memberTestDataOptions.LastName));
            Assert.That(publishedMemberGeneralAttributesEditedDomainEvent.Picture, Is.Not.EqualTo(memberTestDataOptions.Picture));
        }
    }
}

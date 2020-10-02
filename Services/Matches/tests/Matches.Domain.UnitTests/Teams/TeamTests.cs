using Matches.Domain.Team;
using Matches.Domain.Team.Events;
using Matches.Domain.UnitTests.SeedWork;
using NUnit.Framework;

namespace Matches.Domain.UnitTests.Teams
{
    [TestFixture]
    public class TeamTests : TestBase
    {
        [Test]
        public void CreateNewTeam_IsSuccessful()
        {
            var team = Team.Team.Create("name",
                "description",
                new byte[1],
                "manager",
                "league",
                "country",
                1,
                "facebook",
                "instagram",
                Stadium.CreateNew("name", "description", "location"),
                "externalId");

            var teamCreatedDomainEvent = AssertPublishedDomainEvent<TeamCreatedDomainEvent>(team);
            Assert.That(teamCreatedDomainEvent.TeamId, Is.EqualTo(team.Id));
        }

        [Test]
        public void EditGeneralAttributes_IsSuccessful()
        {
            var team = Team.Team.Create("name",
                "description",
                new byte[1],
                "manager",
                "league",
                "country",
                1,
                "facebook",
                "instagram",
                Stadium.CreateNew("name", "description", "location"),
                "externalId");

            var newStadium = Stadium.CreateNew("newName", "newDescription", "newLocation");
            team.EditGeneralAttributes("newName",
                "newDescription",
                new byte[1],
                "newManager",
                "newLeague",
                "newCountry",
                1,
                "newFacebook",
                "newInstagram",
                newStadium,
                "newExternalId");

            var teamGeneralAttributesEditedDomainEvent =
                AssertPublishedDomainEvent<TeamGeneralAttributesEditedDomainEvent>(team);
            Assert.That(teamGeneralAttributesEditedDomainEvent.NewName, Is.EqualTo("newName"));
            Assert.That(teamGeneralAttributesEditedDomainEvent.NewDescription, Is.EqualTo("newDescription"));
            Assert.That(teamGeneralAttributesEditedDomainEvent.NewLogo, Is.EqualTo(new byte[1]));
            Assert.That(teamGeneralAttributesEditedDomainEvent.NewManager, Is.EqualTo("newManager"));
            Assert.That(teamGeneralAttributesEditedDomainEvent.NewLeague, Is.EqualTo("newLeague"));
            Assert.That(teamGeneralAttributesEditedDomainEvent.NewCountry, Is.EqualTo("newCountry"));
            Assert.That(teamGeneralAttributesEditedDomainEvent.NewFormedYear, Is.EqualTo(1));
            Assert.That(teamGeneralAttributesEditedDomainEvent.NewFacebook, Is.EqualTo("newFacebook"));
            Assert.That(teamGeneralAttributesEditedDomainEvent.NewInstagram, Is.EqualTo("newInstagram"));
            Assert.That(teamGeneralAttributesEditedDomainEvent.NewStadium, Is.EqualTo(newStadium));
            Assert.That(teamGeneralAttributesEditedDomainEvent.NewExternalId, Is.EqualTo("newExternalId"));
        }
    }
}
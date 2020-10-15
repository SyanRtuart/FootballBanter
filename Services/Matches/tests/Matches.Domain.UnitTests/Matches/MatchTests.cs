using System;
using System.Threading.Tasks;
using Matches.Domain.Match;
using Matches.Domain.Match.Events;
using Matches.Domain.Team;
using Matches.Domain.UnitTests.SeedWork;
using NUnit.Framework;

namespace Matches.Domain.UnitTests.Matches
{
    [TestFixture]
    public class MatchTests : TestBase
    {
        [Test]
        public void NewMatchCreation_IsSuccessful()
        {
            var match = Match.Match.CreateNew("name",
                new TeamId(Guid.NewGuid()), 
                new TeamId(Guid.NewGuid()), 
                Score.CreateNew("winner", 1, 1),
                "season",
                DateTime.Now,
                "externalId",
                "status");

            var newMatchCreatedDomainEvent = AssertPublishedDomainEvent<MatchCreatedDomainEvent>(match);
            Assert.That(newMatchCreatedDomainEvent.MatchId, Is.EqualTo(match.Id));
        }

        [Test]
        public void EditGeneralAttributes_IsSuccessful()
        {
            var match = Match.Match.CreateNew("name",
                new TeamId(Guid.NewGuid()),
                new TeamId(Guid.NewGuid()),
                Score.CreateNew("winner", 1, 1),
                "season",
                DateTime.Now,
                "externalId",
                "status");

            var newUtcDate = DateTime.Now + TimeSpan.FromDays(1);
            var newScore = Score.CreateNew("winner", 1, 1);
            match.EditGeneralAttributes("newName",
                newUtcDate,
                newScore,
                "newSeason",
                "newStatus");

            var matchGeneralAttributesEditedDomainEvent = AssertPublishedDomainEvent<MatchGeneralAttributesEditedDomainEvent>(match);
            Assert.That(matchGeneralAttributesEditedDomainEvent.NewName, Is.EqualTo("newName"));
            Assert.That(matchGeneralAttributesEditedDomainEvent.NewUtcDate, Is.EqualTo(newUtcDate));
            Assert.That(matchGeneralAttributesEditedDomainEvent.NewScore, Is.EqualTo(newScore));
            Assert.That(matchGeneralAttributesEditedDomainEvent.NewSeason, Is.EqualTo("newSeason"));
            Assert.That(matchGeneralAttributesEditedDomainEvent.NewStatus, Is.EqualTo("newStatus"));
        }
    }
}

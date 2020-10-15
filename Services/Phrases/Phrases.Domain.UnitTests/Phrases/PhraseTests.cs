using System;
using NUnit.Framework;
using Phrases.Domain.Match;
using Phrases.Domain.Phrase.Events;
using Phrases.Domain.Team;
using Phrases.Domain.User;

namespace Phrases.Domain.UnitTests.Phrases
{
    [TestFixture]
    public class PhraseTests : PhraseTestBase
    {
        [Test]
        public void CreatePhrase_WhenAllConditionsAllow_IsSuccessful()
        {
            var phrase = Phrase.Phrase.Create(new MatchId(Guid.NewGuid()), new TeamId(Guid.NewGuid()),
                new UserId(Guid.NewGuid()), "description", true);


            var phraseCreatedDomainEvent = AssertPublishedDomainEvent<PhraseCreatedDomainEvent>(phrase);
            Assert.That(phraseCreatedDomainEvent.PhraseId, Is.EqualTo(phrase.Id));
        }

        [Test]
        public void DeletePhrase_WhenAllConditionsAllow_IsSuccessful()
        {
            var phrase = Phrase.Phrase.Create(new MatchId(Guid.NewGuid()), new TeamId(Guid.NewGuid()),
                new UserId(Guid.NewGuid()), "description", true);

            var userId = new UserId(Guid.NewGuid());

            phrase.Delete(userId);

            var phraseDeletedDomainEvent = AssertPublishedDomainEvent<PhraseDeletedDomainEvent>(phrase);
            Assert.That(phraseDeletedDomainEvent.PhraseId, Is.EqualTo(phrase.Id));
            Assert.That(phraseDeletedDomainEvent.DeletedByUserId, Is.EqualTo(userId));
        }
    }
}
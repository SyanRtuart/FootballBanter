using System;
using NUnit.Framework;
using Phrases.Domain.Phrase.Events;
using Phrases.Domain.UnitTests.SeedWork;

namespace Phrases.Domain.UnitTests.Phrases
{
    [TestFixture]
    public class PhraseTests : PhraseTestBase
    {
        [Test]
        public void CreatePhrase_WhenAllConditionsAllow_IsSuccessful()
        {
            var phrase = Phrase.Phrase.Create(Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), "description", true);


            var phraseCreatedDomainEvent = AssertPublishedDomainEvent<PhraseCreatedDomainEvent>(phrase);
            Assert.That(phraseCreatedDomainEvent.PhraseId, Is.EqualTo(phrase.Id));
        }

        [Test]
        public void DeletePhrase_WhenAllConditionsAllow_IsSuccessful()
        {
            var phrase = Phrase.Phrase.Create(Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), "description", true);


        }

    }
}
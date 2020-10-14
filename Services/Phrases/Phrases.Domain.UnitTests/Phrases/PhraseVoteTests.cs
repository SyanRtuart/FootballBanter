using System;
using NUnit.Framework;
using Phrases.Domain.Phrase.Events;
using Phrases.Domain.Phrase.Rules;
using Phrases.Domain.UnitTests.SeedWork;

namespace Phrases.Domain.UnitTests.Phrases
{
    [TestFixture]
    public class PhraseVoteTests : PhraseTestBase
    {
        [Test] 
        public void Upvote_WhenAllConditionsAllow_IsSuccessful()
        {
            var phraseTestData = CreatePhraseTestData(new PhraseTestDataOptions
            {
                MatchId = Guid.NewGuid(),
                TeamId = Guid.NewGuid(),
                CreatedByUserId = Guid.NewGuid(),
                Description = "description",
            });
            var userId = Guid.NewGuid();

            phraseTestData.Phrase.Upvote(userId);

            var phraseCreatedDomainEvent = AssertPublishedDomainEvent<PhraseVoteHistoryCreatedDomainEvent>(phraseTestData.Phrase);
            Assert.That(phraseCreatedDomainEvent.PhraseId, Is.EqualTo(phraseTestData.Phrase.Id));
            Assert.That(phraseCreatedDomainEvent.Score, Is.EqualTo(1));
            Assert.That(phraseCreatedDomainEvent.UserId, Is.EqualTo(userId));
        }

        [Test]
        public void Upvote_WhenUserUpvoteTwice_IsNotPossible()
        {
            // Arrange
            var userId = Guid.NewGuid();

            // Assert
            AssertBrokenRule<UserCannotUpvoteTwiceRule>(() =>
            {
                // Act
                var phrase = Phrase.Phrase.Create(Guid.NewGuid(), Guid.NewGuid(),
                    userId, "description", true);

                phrase.Upvote(userId);
            });
        }

        [Test]
        public void Downvote_WhenAllConditionsAllow_IsSuccessful()
        {
            var phraseTestData = CreatePhraseTestData(new PhraseTestDataOptions
            {
                MatchId = Guid.NewGuid(),
                TeamId = Guid.NewGuid(),
                CreatedByUserId = Guid.NewGuid(),
                Description = "description",
            });

            var userId = Guid.NewGuid();

            phraseTestData.Phrase.Downvote(userId);

            var phraseCreatedDomainEvent = AssertPublishedDomainEvent<PhraseVoteHistoryCreatedDomainEvent>(phraseTestData.Phrase);
            Assert.That(phraseCreatedDomainEvent.PhraseId, Is.EqualTo(phraseTestData.Phrase.Id));
            Assert.That(phraseCreatedDomainEvent.Score, Is.EqualTo(-1));
            Assert.That(phraseCreatedDomainEvent.UserId, Is.EqualTo(userId));
        }

        [Test]
        public void Downvote_WhenUserDownvoteTwice_IsNotPossible()
        {
            // Arrange
            var userId = Guid.NewGuid();

            // Assert
            AssertBrokenRule<UserCannotDownvoteTwiceRule>(() =>
            {
                // Act
                var phrase = Phrase.Phrase.Create(Guid.NewGuid(), Guid.NewGuid(),
                    userId, "description", true);

                phrase.Downvote(userId);

                phrase.Downvote(userId);
            });
        }
    }
}

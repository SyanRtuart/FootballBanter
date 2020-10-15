using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using NUnit.Framework;
using Phrases.Application.Phrases.Commands.CreatePhrase;
using Phrases.Application.Phrases.Commands.DeletePhrase;
using Phrases.Application.Phrases.Queries.GetPhrase;
using Phrases.IntegrationTests.SeedWork;

namespace Phrases.IntegrationTests.Phrases
{
    [TestFixture]
    public class PhraseTests : TestBase
    {
        [Test]
        public async Task CreatePhraseCommand_Test()
        {
            var phraseId = await PhraseModule.ExecuteCommandAsync(new CreatePhraseCommand(
                PhraseSampleData.MatchId,
                PhraseSampleData.TeamId,
                PhraseSampleData.CreatedByUserId,
                PhraseSampleData.Description,
                PhraseSampleData.Positive));

            var phrase = await PhraseModule.ExecuteQueryAsync(new GetPhraseQuery(phraseId));

            Assert.That(phrase.MatchId, Is.EqualTo(PhraseSampleData.MatchId));
            Assert.That(phrase.TeamId, Is.EqualTo(PhraseSampleData.TeamId));
            Assert.That(phrase.CreatedByUserId, Is.EqualTo(PhraseSampleData.CreatedByUserId));
            Assert.That(phrase.Description, Is.EqualTo(PhraseSampleData.Description));
            Assert.That(phrase.Positive, Is.EqualTo(PhraseSampleData.Positive));

            var connection = new SqlConnection(ConnectionString);
            var messagesList = await OutboxMessagesHelper.GetOutboxMessages(connection);

            Assert.That(messagesList.Count, Is.EqualTo(1));

            var newUserRegisteredNotification = await GetLastOutboxMessage<PhraseCreatedNotification>();

            Assert.That(newUserRegisteredNotification.DomainEvent.MatchId, Is.EqualTo(PhraseSampleData.MatchId));
            Assert.That(newUserRegisteredNotification.DomainEvent.TeamId, Is.EqualTo(PhraseSampleData.TeamId));
            Assert.That(newUserRegisteredNotification.DomainEvent.CreatedByUserId, Is.EqualTo(PhraseSampleData.CreatedByUserId));
            Assert.That(newUserRegisteredNotification.DomainEvent.Description, Is.EqualTo(PhraseSampleData.Description));
            Assert.That(newUserRegisteredNotification.DomainEvent.Positive, Is.EqualTo(PhraseSampleData.Positive));
        }


        [Test]
        public async Task DeletePhraseCommand_Test()
        {
            var phraseId = await PhraseModule.ExecuteCommandAsync(new CreatePhraseCommand(
                PhraseSampleData.MatchId,
                PhraseSampleData.TeamId,
                PhraseSampleData.CreatedByUserId,
                PhraseSampleData.Description,
                PhraseSampleData.Positive));
            var userId = Guid.NewGuid();
            var phraseBeforeDelete = await PhraseModule.ExecuteQueryAsync(new GetPhraseQuery(phraseId));

            await PhraseModule.ExecuteCommandAsync(new DeletePhraseCommand(phraseId, userId));

            var phraseAfterDelete = await PhraseModule.ExecuteQueryAsync(new GetPhraseQuery(phraseId));

            Assert.That(phraseBeforeDelete.MatchId, Is.EqualTo(PhraseSampleData.MatchId));
            Assert.That(phraseAfterDelete == null);
        }
    }
}

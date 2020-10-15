using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Phrases.Application.Phrases.Commands.CreatePhrase;
using Phrases.Application.Phrases.Commands.DownvotePhrase;
using Phrases.Application.Phrases.Commands.UpvotePhrase;
using Phrases.Application.Phrases.Queries.GetPhrase;
using Phrases.IntegrationTests.SeedWork;

namespace Phrases.IntegrationTests.Phrases
{
    [TestFixture]
    public class PhraseVoteTests : TestBase
    {
        [Test]
        public async Task UpvotePhraseCommand_Test()
        {
            var phraseId = await PhraseModule.ExecuteCommandAsync(new CreatePhraseCommand(
                PhraseSampleData.MatchId,
                PhraseSampleData.TeamId,
                PhraseSampleData.CreatedByUserId,
                PhraseSampleData.Description,
                PhraseSampleData.Positive));
            var userId = Guid.NewGuid();

            await PhraseModule.ExecuteCommandAsync(new UpvotePhraseCommand(phraseId, userId));

            var phrase = await PhraseModule.ExecuteQueryAsync(new GetPhraseQuery(phraseId));
            Assert.That(phrase.Score, Is.EqualTo(2));
        }

        [Test]
        public async Task DownvotePhraseCommand_Test()
        {
            var phraseId = await PhraseModule.ExecuteCommandAsync(new CreatePhraseCommand(
                PhraseSampleData.MatchId,
                PhraseSampleData.TeamId,
                PhraseSampleData.CreatedByUserId,
                PhraseSampleData.Description,
                PhraseSampleData.Positive));
            var userId = Guid.NewGuid();

            await PhraseModule.ExecuteCommandAsync(new DownvotePhraseCommand(phraseId, userId));

            var phrase = await PhraseModule.ExecuteQueryAsync(new GetPhraseQuery(phraseId));
            Assert.That(phrase.Score, Is.EqualTo(0));
        }
    }
}

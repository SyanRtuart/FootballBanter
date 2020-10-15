using System.Threading.Tasks;
using Matches.Application.Matches.Commands.CreateMatch;
using Matches.Application.Matches.Commands.EditMatchGeneralAttributes;
using Matches.Application.Matches.Queries.GetMatchById;
using Matches.IntegrationTests.SeedWork;
using NUnit.Framework;

namespace Matches.IntegrationTests.Matches
{
    [TestFixture]
    public class MatchTests : TestBase
    {
        [Test]
        public async Task CreateMatchCommand_Test()
        {
            var matchId = await MatchModule.ExecuteCommandAsync(new CreateMatchCommand(
                MatchSampleData.Name,
                MatchSampleData.HomeTeamId,
                MatchSampleData.AwayTeamId,
                MatchSampleData.UtcDate,
                MatchSampleData.Status,
                MatchSampleData.Score,
                MatchSampleData.Season,
                MatchSampleData.ExternalId));

            var match = await MatchModule.ExecuteQueryAsync(new GetMatchByIdQuery(matchId));

            Assert.That(match.Name, Is.EqualTo(MatchSampleData.Name));
            Assert.That(match.HomeTeamId, Is.EqualTo(MatchSampleData.HomeTeamId));
            Assert.That(match.AwayTeamId, Is.EqualTo(MatchSampleData.AwayTeamId));
            Assert.That(match.UtcDate, Is.EqualTo(MatchSampleData.UtcDate));
            Assert.That(match.Status, Is.EqualTo(MatchSampleData.Status));
            Assert.That(match.ScoreWinner, Is.EqualTo(MatchSampleData.Score.Winner));
            Assert.That(match.ScoreHomeTeam, Is.EqualTo(MatchSampleData.Score.HomeTeam));
            Assert.That(match.ScoreAwayTeam, Is.EqualTo(MatchSampleData.Score.AwayTeam));
            Assert.That(match.Season, Is.EqualTo(MatchSampleData.Season));
            Assert.That(match.ExternalId, Is.EqualTo(MatchSampleData.ExternalId));
        }

        [Test]
        public async Task EditMatchGeneralAttributesCommand_Test()
        {
            var matchId = await MatchModule.ExecuteCommandAsync(new CreateMatchCommand(
                MatchSampleData.Name,
                MatchSampleData.HomeTeamId,
                MatchSampleData.AwayTeamId,
                MatchSampleData.UtcDate,
                MatchSampleData.Status,
                MatchSampleData.Score,
                MatchSampleData.Season,
                MatchSampleData.ExternalId));

            await MatchModule.ExecuteCommandAsync(new EditMatchGeneralAttributesCommand(
                matchId,
                EditMatchSampleData.NewName,
                EditMatchSampleData.NewUtcDate,
                EditMatchSampleData.NewScore,
                EditMatchSampleData.NewSeason,
                EditMatchSampleData.NewStatus));

            var match = await MatchModule.ExecuteQueryAsync(new GetMatchByIdQuery(matchId));

            Assert.That(match.Name, Is.EqualTo(EditMatchSampleData.NewName));
            Assert.That(match.UtcDate, Is.EqualTo(EditMatchSampleData.NewUtcDate));
            Assert.That(match.ScoreWinner, Is.EqualTo(EditMatchSampleData.NewScore.Winner));
            Assert.That(match.ScoreHomeTeam, Is.EqualTo(EditMatchSampleData.NewScore.HomeTeam));
            Assert.That(match.ScoreAwayTeam, Is.EqualTo(EditMatchSampleData.NewScore.AwayTeam));
            Assert.That(match.Season, Is.EqualTo(EditMatchSampleData.NewSeason));
            Assert.That(match.Status, Is.EqualTo(EditMatchSampleData.NewStatus));
        }
    }
}

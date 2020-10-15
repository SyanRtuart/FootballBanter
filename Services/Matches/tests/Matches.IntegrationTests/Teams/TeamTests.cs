using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matches.Application.Teams.Commands.CreateTeam;
using Matches.Application.Teams.Commands.EditTeamGeneralAttributes;
using Matches.Application.Teams.Queries.GetTeam;
using Matches.IntegrationTests.SeedWork;
using NUnit.Framework;

namespace Matches.IntegrationTests.Teams
{
    [TestFixture]
    public class TeamTests : TestBase
    {
        [Test]
        public async Task CreateTeamCommand_Test()
        {
            var teamId = await MatchModule.ExecuteCommandAsync(new CreateTeamCommand(
                TeamSampleData.Name,
                TeamSampleData.Description,
                TeamSampleData.Logo,
                TeamSampleData.Manager,
                TeamSampleData.League,
                TeamSampleData.Country,
                TeamSampleData.FormedYear,
                TeamSampleData.Facebook,
                TeamSampleData.Instagram,
                TeamSampleData.Stadium,
                TeamSampleData.ExternalId));

            var team = await MatchModule.ExecuteQueryAsync(new GetTeamQuery(teamId));

            Assert.That(team.Name, Is.EqualTo(TeamSampleData.Name));
            Assert.That(team.Description, Is.EqualTo(TeamSampleData.Description));
            Assert.That(team.Logo, Is.EqualTo(TeamSampleData.Logo));
            Assert.That(team.Manager, Is.EqualTo(TeamSampleData.Manager));
            Assert.That(team.League, Is.EqualTo(TeamSampleData.League));
            Assert.That(team.Country, Is.EqualTo(TeamSampleData.Country));
            Assert.That(team.FormedYear, Is.EqualTo(TeamSampleData.FormedYear));
            Assert.That(team.Facebook, Is.EqualTo(TeamSampleData.Facebook));
            Assert.That(team.Instagram, Is.EqualTo(TeamSampleData.Instagram));
            Assert.That(team.StadiumName, Is.EqualTo(TeamSampleData.Stadium.Name));
            Assert.That(team.StadiumDescription, Is.EqualTo(TeamSampleData.Stadium.Description));
            Assert.That(team.StadiumLocation, Is.EqualTo(TeamSampleData.Stadium.Location));
            Assert.That(team.ExternalId, Is.EqualTo(TeamSampleData.ExternalId));
        }

        [Test]
        public async Task EditTeamGeneralAttributesCommand_Test()
        {
            var teamId = await MatchModule.ExecuteCommandAsync(new CreateTeamCommand(
                TeamSampleData.Name,
                TeamSampleData.Description,
                TeamSampleData.Logo,
                TeamSampleData.Manager,
                TeamSampleData.League,
                TeamSampleData.Country,
                TeamSampleData.FormedYear,
                TeamSampleData.Facebook,
                TeamSampleData.Instagram,
                TeamSampleData.Stadium,
                TeamSampleData.ExternalId));

            await MatchModule.ExecuteCommandAsync(new EditTeamGeneralAttributesCommand(
                teamId,
                EditTeamSampleData.NewName,
                EditTeamSampleData.NewDescription,
                EditTeamSampleData.NewLogo,
                EditTeamSampleData.NewManager,
                EditTeamSampleData.NewLeague,
                EditTeamSampleData.NewCountry,
                EditTeamSampleData.NewFormedYear,
                EditTeamSampleData.NewFacebook,
                EditTeamSampleData.NewInstagram,
                EditTeamSampleData.NewStadium,
                EditTeamSampleData.NewExternalId));

            var team = await MatchModule.ExecuteQueryAsync(new GetTeamQuery(teamId));

            Assert.That(team.Name, Is.EqualTo(EditTeamSampleData.NewName));
            Assert.That(team.Description, Is.EqualTo(EditTeamSampleData.NewDescription));
            Assert.That(team.Logo, Is.EqualTo(EditTeamSampleData.NewLogo));
            Assert.That(team.Manager, Is.EqualTo(EditTeamSampleData.NewManager));
            Assert.That(team.League, Is.EqualTo(EditTeamSampleData.NewLeague));
            Assert.That(team.Country, Is.EqualTo(EditTeamSampleData.NewCountry));
            Assert.That(team.FormedYear, Is.EqualTo(EditTeamSampleData.NewFormedYear));
            Assert.That(team.Facebook, Is.EqualTo(EditTeamSampleData.NewFacebook));
            Assert.That(team.Instagram, Is.EqualTo(EditTeamSampleData.NewInstagram));
            Assert.That(team.StadiumName, Is.EqualTo(EditTeamSampleData.NewStadium.Name));
            Assert.That(team.StadiumDescription, Is.EqualTo(EditTeamSampleData.NewStadium.Description));
            Assert.That(team.StadiumLocation, Is.EqualTo(EditTeamSampleData.NewStadium.Location));
            Assert.That(team.ExternalId, Is.EqualTo(EditTeamSampleData.NewExternalId));
        }
    }
}

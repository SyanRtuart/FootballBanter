using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Matches.Application.Configuration.Commands;
using Matches.Application.Contracts;
using Matches.Application.Matches.Commands.CreateMatch;
using Matches.Application.Matches.Commands.EditMatchGeneralAttributes;
using Matches.Application.Matches.Queries.GetAllMatches;
using Matches.Application.Matches.SharedModels;
using Matches.Application.Teams.Queries.GetAllTeams;
using Matches.Domain.Match;
using Matches.Infrastructure.Configuration.Processing;
using MediatR;

namespace Matches.Infrastructure.Configuration.Integration.Matches.SyncMatches
{
    public class SyncMatchesCommandHandler : ICommandHandler<SyncMatchesCommand>
    {
        private readonly IIntegrationService _integrationService;
        private List<TeamDto> _teamsInDb;
        private List<MatchDto> _matchesInDb;
        private readonly string _splLeagueId = "4330"; //TODO: Expand Leagues

        public SyncMatchesCommandHandler(IIntegrationService integrationService)
        {
            _integrationService = integrationService;
        }

        public async Task<Unit> Handle(SyncMatchesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _teamsInDb = await QueryExecutor.Execute(new GetAllTeamsQuery());
                _matchesInDb = await QueryExecutor.Execute(new GetAllMatchesQuery());

                var next15Matches = await _integrationService.GetNext15Matches(_splLeagueId);
                var last15Matches = await _integrationService.GetLast15Matches(_splLeagueId);

                var matches = new List<MatchResponse>();
                matches.AddRange(next15Matches.Matches);
                matches.AddRange(last15Matches.Matches);

                foreach (var match in matches)
                {
                    var homeTeam = _teamsInDb.SingleOrDefault(x => x.ExternalId == match.idHomeTeam);
                    var awayTeam = _teamsInDb.SingleOrDefault(x => x.ExternalId == match.idAwayTeam);

                    if (!MatchExistsInDb(out var existingMatchId, match.idEvent))
                    {
                        await AddMatch(match, homeTeam, awayTeam);
                    }
                    else
                    {
                        await EditMatch(match, existingMatchId);
                    }
                }

                return Unit.Value;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private bool MatchExistsInDb(out Guid id, string matchIdEvent)
        {
            foreach (var match in _matchesInDb)
            {
                if (Equals(match.ExternalId, matchIdEvent))
                {
                    id = match.Id;
                    return true;
                }
            }

            id = Guid.Empty;
            return false;
        }

        private async Task AddMatch(MatchResponse match, TeamDto homeTeam, TeamDto awayTeam)
        {
            var date = TryParseDate(match.dateEvent, match.strTime);

            var score = Score.CreateNew("Placeholder", TryParseInt(match.intHomeScore), TryParseInt(match.intAwayScore));

            var command = new CreateMatchCommand(match.strEvent, homeTeam.Id, awayTeam.Id, date.GetValueOrDefault(), "Placeholder",
                score, match.strSeason,
                match.idEvent);

            await CommandsExecutor.Execute(command);
        }

        private async Task EditMatch(MatchResponse match, Guid existingMatchId)
        {
            var date = TryParseDate(match.dateEvent, match.strTime);
            var score = Score.CreateNew("Placeholder", TryParseInt(match.intHomeScore), TryParseInt(match.intAwayScore));

            var command = new EditMatchGeneralAttributesCommand(existingMatchId, match.strEvent, date.GetValueOrDefault(), 
               score, match.strSeason, "Placeholder");

            await CommandsExecutor.Execute(command);
        }

        public static int? TryParseInt(string text)
        {
            return int.TryParse(text, out var validInt) ? validInt : (int?)null;
        }

        public static DateTime? TryParseDate(string dateString, string timeString)
        {
            return DateTime.TryParse(dateString + " " + timeString, out var validDate) ? validDate : (DateTime?)null;
        }
    }
}
using System;
using Matches.Application.Contracts;
using Matches.Domain.Match;
using MediatR;

namespace Matches.Application.Matches.Commands.CreateMatch
{
    public class CreateMatchCommand : CommandBase<Guid>
    {
        public CreateMatchCommand(string name, Guid homeTeamId, Guid awayTeamId, DateTime utcDate, string status, Score score, string season, string externalId)
        {
            Name = name;
            HomeTeamId = homeTeamId;
            AwayTeamId = awayTeamId;
            UtcDate = utcDate;
            Score = score;
            Status = status;
            Season = season;
            ExternalId = externalId;
        }

        public string Name { get; set; }
        public Guid HomeTeamId { get; set; }
        public Guid AwayTeamId { get; set; }
        public DateTime UtcDate { get; set; }
        public string Status { get; set; }
        public Score Score { get; set; }
        public string Season { get; set; }
        public string ExternalId { get; set; }
    }
}
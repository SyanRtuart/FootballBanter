using System;
using Matches.Application.Contracts;
using Matches.Domain.Match;
using MediatR;

namespace Matches.Application.Matches.Commands.EditMatchGeneralAttributes
{
    public class EditMatchGeneralAttributesCommand : CommandBase<Unit>
    {
        public EditMatchGeneralAttributesCommand(Guid matchId, string name, DateTime utcDate, Score score, string season, string status)
        {
            MatchId = matchId;
            Name = name;
            UtcDate = utcDate;
            Score = score;
            Season = season;
            Status = status;
        }
        public Guid MatchId { get; set; }
        public string Name { get; set; }
        public DateTime UtcDate { get; set; }
        public Score Score { get; set; }
        public string Season { get; set; }
        public string Status { get; set; }

    }
}
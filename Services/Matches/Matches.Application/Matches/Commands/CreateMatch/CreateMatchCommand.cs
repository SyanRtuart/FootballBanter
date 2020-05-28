using System;
using MediatR;

namespace Matches.Application.Matches.Commands.CreateMatch
{
    public class CreateMatchCommand : IRequest<Guid>
    {
        public CreateMatchCommand(Guid homeTeamId, Guid awayTeamId, DateTime utcDate, string status, string scoreWinner,
            int scoreHomeTeam, int scoreAwayTeam)
        {
            HomeTeamId = homeTeamId;
            AwayTeamId = awayTeamId;
            UtcDate = utcDate;
            Status = status;
            ScoreWinner = scoreWinner;
            ScoreHomeTeam = scoreHomeTeam;
            ScoreAwayTeam = scoreAwayTeam;
        }

        public Guid HomeTeamId { get; set; }
        public Guid AwayTeamId { get; set; }
        public DateTime UtcDate { get; set; }
        public string Status { get; set; }
        public string ScoreWinner { get; set; }
        public int ScoreHomeTeam { get; set; }
        public int ScoreAwayTeam { get; set; }
    }
}
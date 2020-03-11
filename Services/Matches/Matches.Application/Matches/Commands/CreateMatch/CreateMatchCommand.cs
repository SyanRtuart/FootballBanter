using System;
using MediatR;

namespace Matches.Application.Matches.Commands.CreateMatch
{
    public class CreateMatchCommand : IRequest<bool>
    {
        public CreateMatchCommand(int homeTeamId, int awayTeamId, DateTime utcDate, string status, string scoreWinner, int scoreHomeTeam, int scoreAwayTeam)
        {
            HomeTeamId = homeTeamId;
            AwayTeamId = awayTeamId;
            UtcDate = utcDate;
            Status = status;
            ScoreWinner = scoreWinner;
            ScoreHomeTeam = scoreHomeTeam;
            ScoreAwayTeam = scoreAwayTeam;
        }

        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public DateTime UtcDate { get; set; }
        public string Status { get; set; }
        public string ScoreWinner { get; set; }
        public int ScoreHomeTeam { get; set; }
        public int ScoreAwayTeam { get; set; }
        
    }
}

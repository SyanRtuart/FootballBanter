using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Matches.Application.Matches.Queries.GetRecentMatchesByTeam
{
    public class MatchDto
    {
        public int Id { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string ScoreWinner { get; set; }
        public int ScoreHomeTeam { get; set; }
        public int ScoreAwayTeam { get; set; }
    }
}

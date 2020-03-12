﻿using System;
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
        public string Winner { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matches.Application.Matches.Commands.CreateMatch
{
    public class CreateMatchRequest
    {
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public DateTime UtcDate { get; set; }
        public string Status { get; set; }
        public string ScoreWinner { get; set; }
        public int ScoreHomeTeam { get; set; }
        public int ScoreAwayTeam { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phrases.Application.Members.Queries.GetMember
{
    public class MemberDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int BanterScore { get; set; }
        public int CommentScore { get; set; }
        public byte[] Picture { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phrases.Application.Configuration.Queries;

namespace Phrases.Application.Members.Queries.GetMember
{
    public class GetMemberQuery : QueryBase<MemberDto>
    {
        public GetMemberQuery(Guid memberId)
        {
            MemberId = memberId;
        }

        public Guid MemberId { get; set; }
    }
}

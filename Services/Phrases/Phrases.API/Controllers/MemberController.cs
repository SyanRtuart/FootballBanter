using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Phrases.Application.Contracts;
using Phrases.Application.Members.Queries.GetMember;

namespace Phrases.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly IPhrasesModule _phrasesModule;

        public MemberController(IPhrasesModule phrasesModule)
        {
            _phrasesModule = phrasesModule;
        }

        [HttpGet("{memberId}")]
        public async Task<MemberDto> GetMember([FromRoute] Guid memberId)
        {
            return await _phrasesModule.ExecuteQueryAsync(new GetMemberQuery(memberId));
        }
        
   
    }
}

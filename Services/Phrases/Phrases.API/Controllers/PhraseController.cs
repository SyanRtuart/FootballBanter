using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Phrases.Application.Phrases.Commands.CreatePhrase;

namespace Phrases.API.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class PhraseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PhraseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePhraseAsync(CreatePhraseRequest request)
        {
            return Ok(await _mediator.Send(new CreatePhraseCommand(request.MatchId, request.TeamId, request.Description, request.Positive)));
        }
    }
}

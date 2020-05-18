using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Phrases.Application.Phrases.Commands.CreatePhrase;
using Phrases.Application.Phrases.Commands.DeletePhrase;
using Phrases.Application.Phrases.Queries.GetPhrasesForMatch;

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
            await _mediator.Send(new CreatePhraseCommand(request.MatchId, request.TeamId, request.Description,
                request.Positive));

            return Ok();
        }

        [HttpGet]
        public async Task<List<PhraseDto>> GetPhrasesForMatch(int matchId)
        {
            return await _mediator.Send(new GetPhrasesForMatchQuery(matchId));
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePhraseAsync(int phraseId)
        {
            await _mediator.Send(new DeletePhraseCommand(phraseId));

            return Ok();
        }

    }
}
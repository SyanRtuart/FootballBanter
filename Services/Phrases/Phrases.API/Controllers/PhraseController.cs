using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Phrases.Application.Phrases.Commands.CreatePhrase;
using Phrases.Application.Phrases.Commands.DeletePhrase;
using Phrases.Application.Phrases.Commands.DownvotePhrase;
using Phrases.Application.Phrases.Commands.UpvotePhrase;
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
        public async Task<Guid> CreatePhraseAsync(CreatePhraseRequest request)
        {
            var id = await _mediator.Send(new CreatePhraseCommand(request.MatchId, request.TeamId, request.Description,
                request.Positive));

            return id;
        }

        [HttpGet]
        public async Task<List<PhraseDto>> GetPhrasesForMatchAsync(Guid matchId)
        {
            return await _mediator.Send(new GetPhrasesForMatchQuery(matchId));
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePhraseAsync(Guid phraseId)
        {
            await _mediator.Send(new DeletePhraseCommand(phraseId));

            return Ok();
        }

        [HttpPut]
        [Route("{phraseId:int}/upvote")]
        public async Task<IActionResult> UpvotePhraseAsync(Guid phraseId)
        {
            await _mediator.Send(new UpvotePhraseCommand(phraseId));

            return Ok();
        }

        [HttpPut]
        [Route("{phraseId:int}/downvote")]
        public async Task<IActionResult> DownvotePhraseAsync(Guid phraseId)
        {
            await _mediator.Send(new DownvotePhraseCommand(phraseId));

            return Ok();
        }
    }
}
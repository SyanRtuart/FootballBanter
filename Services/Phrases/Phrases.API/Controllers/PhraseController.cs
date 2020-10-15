using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Phrases.Application.Contracts;
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
        private readonly IPhrasesModule _phrasesModule;

        public PhraseController(IPhrasesModule phrasesModule)
        {
            _phrasesModule = phrasesModule;
        }

        [HttpPost]
        public async Task<Guid> CreatePhraseAsync(CreatePhraseRequest request)
        {
            var id = await _phrasesModule.ExecuteCommandAsync(new CreatePhraseCommand(request.MatchId, request.TeamId,
                request.CreatedByUserId, request.Description,
                request.Positive));

            return id;
        }

        [HttpGet]
        public async Task<List<PhraseDto>> GetPhrasesForMatchAsync(Guid matchId)
        {
            return await _phrasesModule.ExecuteQueryAsync(new GetPhrasesForMatchQuery(matchId));
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePhraseAsync(Guid phraseId, Guid userId)
        {
            await _phrasesModule.ExecuteCommandAsync(new DeletePhraseCommand(phraseId, userId));

            return Ok();
        }

        [HttpPut]
        [Route("{phraseId:guid}/upvote")]
        public async Task<IActionResult> UpvotePhraseAsync(Guid phraseId, Guid userId)
        {
            await _phrasesModule.ExecuteCommandAsync(new UpvotePhraseCommand(phraseId, userId));

            return Ok();
        }

        [HttpPut]
        [Route("{phraseId:guid}/downvote")]
        public async Task<IActionResult> DownvotePhraseAsync(Guid phraseId, Guid userId)
        {
            await _phrasesModule.ExecuteCommandAsync(new DownvotePhraseCommand(phraseId, userId ));

            return Ok();
        }
    }
}
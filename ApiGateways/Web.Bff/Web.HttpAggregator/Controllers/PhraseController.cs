using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Web.HttpAggregator.Models.Phrase;
using Web.HttpAggregator.Services.Phrase;

namespace Web.HttpAggregator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowOrigin")]
    public class PhraseController : ControllerBase
    {
        private readonly IPhraseApiClient _phraseApiClient;

        public PhraseController(IPhraseApiClient phraseApiClient)
        {
            _phraseApiClient = phraseApiClient;
        }

        [HttpGet]
        [Route("getPhrases")]
        public async Task<List<Phrase>> GetPhrases(Guid matchId)
        {
            var response = await _phraseApiClient.GetPhrasesAsync(matchId);

            var phrases = new List<Phrase>();

            foreach (var phraseData in response)
                phrases.Add(new Phrase(phraseData.Id, phraseData.Description, phraseData.Positive, phraseData.Score));

            return phrases;
        }

        [HttpPost]
        [Route("createPhrase")]
        public async Task<Guid> CreatePhrase(CreatePhraseRequest request)
        {
            var id = await _phraseApiClient.CreatePhrase(request);

            return id;
        }

        [HttpPut]
        [Route("upvotePhrase")]
        public async Task<IActionResult> UpvotePhrase(Guid phraseId)
        {
            var request = new UpvotePhraseRequest(phraseId);

            await _phraseApiClient.UpvotePhrase(request);

            return Ok();
        }

        [HttpPut]
        [Route("downvotePhrase")]
        public async Task<IActionResult> DownvotePhrase(Guid phraseId)
        {
            var request = new DownvotePhraseRequest(phraseId);

            await _phraseApiClient.DownvotePhrase(request);

            return Ok();
        }
    }
}
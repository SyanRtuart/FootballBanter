﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task<List<Phrase>> GetPhrases(int matchId)
        {
            var response = await _phraseApiClient.GetPhrasesAsync(matchId);

            var phrases = new List<Phrase>();

            foreach (var phraseData in response)
            {
                phrases.Add(new Phrase(phraseData.Id, phraseData.Description, phraseData.Positive));
            }

            return phrases;
        }

        [HttpPost]
        [Route("createPhrase")]
        public async Task<IActionResult> CreatePhrase(CreatePhraseRequest request)
        {
            await _phraseApiClient.CreatePhrase(request);

            return Ok();
        }

    }
}
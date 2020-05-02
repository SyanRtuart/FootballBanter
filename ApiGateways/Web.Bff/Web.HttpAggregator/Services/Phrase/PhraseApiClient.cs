using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Web.HttpAggregator.Config;
using Web.HttpAggregator.Models.Phrase;

namespace Web.HttpAggregator.Services.Phrase
{
    public class PhraseApiClient : IPhraseApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly UrlsConfig _urls;

        public PhraseApiClient(HttpClient httpClient, IOptions<UrlsConfig> config)
        {
            _httpClient = httpClient;
            _urls = config.Value;
        }

        public Task CreatePhrase(int matchId, string description, bool positive)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PhraseData>> GetPhrasesAsync(int matchId)
        {
            var url = _urls.Phrase + UrlsConfig.PhraseOperations.GetPhrases(matchId);

            var response = await _httpClient.GetAsync(url);

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<PhraseData>>(content);
        }
    }
}

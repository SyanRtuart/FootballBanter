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

        public async Task CreatePhrase(CreatePhraseRequest request)
        {
            var url = _urls.Phrase + UrlsConfig.PhraseOperations.CreatePhrase;

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);

            response.EnsureSuccessStatusCode();
        }
        
        public async Task<List<PhraseData>> GetPhrasesAsync(Guid matchId)
        {
            var url = _urls.Phrase + UrlsConfig.PhraseOperations.GetPhrases(matchId);

            var response = await _httpClient.GetAsync(url);

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<PhraseData>>(content);
        }
        public async Task UpvotePhrase(UpvotePhraseRequest request)
        {
            var url = _urls.Phrase + UrlsConfig.PhraseOperations.UpvotePhrase(request.PhraseId);

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(url, content);

            response.EnsureSuccessStatusCode();
        }

        public async Task DownvotePhrase(DownvotePhraseRequest request)
        {
            var url = _urls.Phrase + UrlsConfig.PhraseOperations.DownvotePhrase(request.PhraseId);

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(url, content);

            response.EnsureSuccessStatusCode();
        }

    }
}

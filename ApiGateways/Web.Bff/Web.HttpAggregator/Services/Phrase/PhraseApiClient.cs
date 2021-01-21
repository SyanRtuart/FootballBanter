using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Web.HttpAggregator.Config;
using Web.HttpAggregator.Models.Phrase;
using Web.HttpAggregator.Models.Phrase.Member;

namespace Web.HttpAggregator.Services.Phrase
{
    public class PhraseApiClient : IPhraseApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly UrlsConfig _urls;

        public PhraseApiClient(HttpClient httpClient, IOptions<UrlsConfig> config, IExecutionContextAccessor executionContextAccessor)
        {
            _httpClient = httpClient;
            _urls = config.Value;
            
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", executionContextAccessor.GetTokenAsync().Result);
        }

        public async Task<Guid> CreatePhrase(CreatePhraseRequest request)
        {
            var url = _urls.Phrase + UrlsConfig.PhraseOperations.CreatePhrase;

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);

            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Guid>(responseContent);
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

        public async Task<MemberData> GetMember(GetMemberRequest request)
        {
            var url = _urls.Phrase + UrlsConfig.PhraseOperations.GetMember(request.MemberId);

            var response = await _httpClient.GetAsync(url);

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<MemberData>(content);
        }
    }
}
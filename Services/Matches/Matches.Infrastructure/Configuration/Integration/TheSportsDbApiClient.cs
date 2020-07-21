using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Matches.Application.Contracts;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Matches.Infrastructure.Configuration.Integration
{
    public class TheSportsDbApiClient : IIntegrationService
    {
        public string ApiKey { get; set; }
        public string Url { get; set; }

        private readonly HttpClient _httpClient;
        private readonly UrlsConfig _urls;
        public TheSportsDbApiClient(HttpClient httpClient, UrlsConfig urls)
        {
            _httpClient = httpClient;
            _urls = urls;
        }

        public async Task<TeamsResponse> GetTeams()
        {
            var url = _urls.GetAllTeams();

            var response  = await _httpClient.GetAsync(url);

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TeamsResponse>(content);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Matches.Application.Contracts;
using Matches.Infrastructure.Configuration.Integration.Matches;
using Matches.Infrastructure.Configuration.Integration.Teams;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Matches.Infrastructure.Configuration.Integration
{
    public class TheSportsDbApiClient : IIntegrationService
    {
        private readonly HttpClient _httpClient;
        private readonly UrlsConfig _urls;
        public TheSportsDbApiClient(HttpClient httpClient, UrlsConfig urls)
        {
            _httpClient = httpClient;
            _urls = urls;
        }

        public async Task<TeamsResponse> GetTeams(string country)
        {
            var url = _urls.GetAllTeams(country);

            var response  = await _httpClient.GetAsync(url);

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TeamsResponse>(content);
        }

        public async Task<MatchesResponse> GetNext15Matches(string leagueId)
        {
            var url = _urls.GetNext15Matches(leagueId);

            var response = await _httpClient.GetAsync(url);

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<MatchesResponse>(content);
        }

        public async Task<MatchesResponse> GetLast15Matches(string leagueId)
        {
            var url = _urls.GetLast15Matches(leagueId);

            var response = await _httpClient.GetAsync(url);

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<MatchesResponse>(content);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Web.HttpAggregator.Config;
using Web.HttpAggregator.Models.Match;

namespace Web.HttpAggregator.Services.Match
{
    public class MatchApiClient : IMatchApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly UrlsConfig _urls;

        public MatchApiClient(HttpClient httpClient, IOptions<UrlsConfig> config)
        {
            _httpClient = httpClient;
            _urls = config.Value;
        }

        public async Task<List<TeamData>> GetTeamsAsync()
        {
            var url = _urls.Match + UrlsConfig.MatchOperations.GetTeams();

            var response = await _httpClient.GetAsync(url);

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<TeamData>>(content);
        }

        public async Task<List<MatchData>> GetRecentMatchesAsync(Guid teamId)
        {
            var url = _urls.Match + UrlsConfig.MatchOperations.GetRecentMatches(teamId);

            var response = await _httpClient.GetAsync(url);

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<MatchData>>(content);
        }

        public async Task<MatchData> GetMatch(Guid id)
        {
            var url = _urls.Match + UrlsConfig.MatchOperations.GetMatch(id);

            var response = await _httpClient.GetAsync(url);

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<MatchData>(content);
        }
    }
}
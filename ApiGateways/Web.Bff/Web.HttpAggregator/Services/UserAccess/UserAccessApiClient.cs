using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Web.HttpAggregator.Config;
using Web.HttpAggregator.Exceptions;
using Web.HttpAggregator.Models;
using Web.HttpAggregator.Models.UserAccess;

namespace Web.HttpAggregator.Services.UserAccess
{
    public class UserAccessApiClient : IUserAccessApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IdentityConfig _identity;
        private readonly UrlsConfig _urls;

        public UserAccessApiClient(HttpClient httpClient, IOptions<UrlsConfig> urlConfig,
            IOptions<IdentityConfig> identityConfig, IExecutionContextAccessor executionContextAccessor)
        {
            _httpClient = httpClient;
            _urls = urlConfig.Value;
            _identity = identityConfig.Value;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", executionContextAccessor.GetTokenAsync().Result);
        }

        public async Task RegisterNewUserAsync(RegisterNewUserRequest request)
        {
            var url = _urls.UserAccess + UrlsConfig.UserAccessOperations.RegisterNewUser;

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                throw new BusinessRuleValidationException(responseContent);
            }
        }

        public async Task<TokenResponse> LoginAsync(LoginRequest request)
        {
            var url = _urls.UserAccess + UrlsConfig.UserAccessOperations.Login;

            var tokenResponse = await _httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = url,
                UserName = request.Email,
                Password = request.Password,
                ClientId = _identity.ClientId,
                ClientSecret = _identity.ClientSecret,
                Scope = _identity.Scope
            });

            return tokenResponse;
        }

        public Task LogoutAsync(string token)
        {
            throw new NotImplementedException();
        }

        public async Task<UserData> GetUser(string email)
        {
            var url = _urls.UserAccess + UrlsConfig.UserAccessOperations.GetUser(email);

            var response = await _httpClient.GetAsync(url);

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<UserData>(content);
        }

        public async Task UploadPicture(UploadPictureRequest request)
        {
            var url = _urls.UserAccess + UrlsConfig.UserAccessOperations.UploadPicture(request.UserId);

            var formData = new MultipartFormDataContent();

            await using (var ms = new MemoryStream())
            {
                await request.Picture .CopyToAsync(ms);
                formData.Add(new StreamContent(new MemoryStream(ms.ToArray())), "Image", request.Picture.FileName);
            }

            var response = await _httpClient.PostAsync(url, formData);

            response.EnsureSuccessStatusCode();
        }

        public async Task ChangePasswordAsync(ChangePasswordRequest request)
        {
            var url = _urls.UserAccess + UrlsConfig.UserAccessOperations.ChangePassword(request.UserId);

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var apiException = JsonConvert.DeserializeObject<ApiExceptionResponse>(responseContent);
                throw new BusinessRuleValidationException(apiException.Detail);
            }
        }
    }
}
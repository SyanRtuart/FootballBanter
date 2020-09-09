using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Web.HttpAggregator.Models.UserAccess;

namespace Web.HttpAggregator.Services.UserAccess
{
    public interface IUserAccessApiClient
    {
        Task RegisterNewUserAsync(RegisterNewUserRequest request);

        Task<TokenResponse> LoginAsync(LoginRequest request);

        Task LogoutAsync(string token);

        Task<UserData> GetUser(string email);

        Task UploadPicture(UploadPictureRequest request);
    }
}
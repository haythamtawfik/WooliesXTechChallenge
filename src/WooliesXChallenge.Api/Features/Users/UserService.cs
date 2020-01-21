using Microsoft.Extensions.Options;
using WooliesXChallenge.Api.Features.Common;

namespace WooliesXChallenge.Api.Features.Users
{
    public interface IUserService
    {
        GetUserResponse GetUser();
    }

    public class UserService:IUserService
    {
        private readonly ApiSettings _settings;

        public UserService(IOptions<ApiSettings> settings)
        {
            _settings = settings.Value;
        }

        public GetUserResponse GetUser()
        {
            return new GetUserResponse
            {
                Name = _settings.Name,
                Token = _settings.Token
            };
        }
    }
}

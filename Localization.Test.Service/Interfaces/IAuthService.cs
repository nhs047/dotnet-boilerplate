using Localization.Test.Common.RequestModels;
using Localization.Test.Common.ResponseModels;
using Localization.Test.Infrastructure.Models;

namespace Localization.Test.Service.Interfaces
{
    public interface IAuthService
    {
        LoginResponse Login(LoginRequest loginRequest);
        string GetToken(User user);
    }
}

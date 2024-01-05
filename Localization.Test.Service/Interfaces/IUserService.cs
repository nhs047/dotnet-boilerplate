using Localization.Test.Common.RequestModels;
using Localization.Test.Common.ResponseModels;
using Localization.Test.Infrastructure.Models;

namespace Localization.Test.Service.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers(SearchQueryRequest searchQueryRequest);
        RegistrationResponse Register(RegistrationRequest registrationRequest);
        User? GetUser(int id);
        User? GetUserWithRole(int id);
        User? UpdateUser(UpdateUserRequest userRequest);
    }
}

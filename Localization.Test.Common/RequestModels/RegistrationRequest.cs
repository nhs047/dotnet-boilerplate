using Localization.Test.Common.Models;

namespace Localization.Test.Common.RequestModels
{
    public class RegistrationRequest: UserBase
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

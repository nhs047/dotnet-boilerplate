using Localization.Test.Common.Models;

namespace Localization.Test.Common.RequestModels
{
    public class UpdateUserRequest : UserBase
    {
        public int Id { get; set; }
    }
}

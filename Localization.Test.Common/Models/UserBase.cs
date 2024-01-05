using Localization.Test.Common.Constants;

namespace Localization.Test.Common.Models
{
    public class UserBase
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string Email { get; set; }
        public string? MobileNumber { get; set; }
        public string Culture { get; set; } = Keys.DefaultCulture;
    }
}

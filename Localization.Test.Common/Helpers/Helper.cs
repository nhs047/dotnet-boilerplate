using Localization.Test.Common.Constants;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Localization.Test.Common.Helpers
{
    public class Helper
    {
        public static bool IsEmail(string email)
        {
            return Regex.IsMatch(
                email,
                Keys.EMAIL_REGEX,
                RegexOptions.IgnoreCase);
        }
        public static bool IsPasswordCaseMatched(string password)
        {
            return Regex.IsMatch(
                password,
                Keys.PASSWORD_REGEX);
        }
        public static bool IsExistInCultureType(string culture)
        {
            return Keys.CultureType.Any(x => x == culture);
        }

        public static void SetCulture(string culture)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
        }
    }
}

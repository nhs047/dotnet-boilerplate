namespace Localization.Test.Common.Constants
{
    public class Keys
    {
        #region Miscilinious
        public const string V1 = "v1";
        public const string Comma = ",";
        public const string TOKEN = "Token";
        public const string CommaEscaped = "\\,";

        public const string ResourcesString = "Resources";
        public const string EmailManagerString = "Email Manager";

        public const string LocalizationAPI = "Localization API";
        public const string SharedResourcePath = "SharedResource";
        public const string MailHasSent = "Event has succesfully sent mail";
        public const string JWTTokenRequired = "Please insert JWT with Bearer into field";
        public const string ConfigureEmailSettingProperly = "Please Configure Email Settings Properly!";
        public const string Logo = "data:image/png;base64,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";

        #endregion

        #region Token
        public const string TOKEN_HEADER = "token";
        public const string BEARER_WITH_SPACE = "Bearer ";
        public const string AUTHORIZATION_HEADER = "authorization";
        #endregion

        #region Culture
        public const string GermanCulture = "de-DE";
        public const string DefaultCulture = "en-US";
        public static readonly List<string> CultureType = new List<string>
        {
            "en-US",
            "de-DE"
        };
        #endregion


        #region Placeholders
        public const string AppNamePlaceholder = "[*AppName*]";
        public const string FullnamePlaceholder = "[*Fullname*]";
        public const string SupportiveTextPlaceholder = "[*SupportiveText*]";
        public const string AppLinkPlaceholder = "[*AppLink*]";
        public const string CompanyNamePlaceholder = "[*CompanyName*]";
        #endregion

        #region Response
        public const string IS_REQUIRED = " is required";
        #endregion

        #region Regex
        public const string PASSWORD_REGEX = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
        public const string EMAIL_REGEX = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
        #endregion
    }
}

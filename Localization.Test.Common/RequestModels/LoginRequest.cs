namespace Localization.Test.Common.RequestModels
{
    /// <summary>
    /// A Login Request.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// The user's username.
        /// </summary>
        /// <example>nhs047@gmail.com</example>
        public string? Username { get; set; }
        /// <summary>
        /// The user's password.
        /// </summary>
        /// <example>1qazZAQ!</example>
        public string? Password { get; set; }
    }
}

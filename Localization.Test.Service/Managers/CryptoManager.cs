using Microsoft.AspNet.Identity;
using System.Text;

namespace Localization.Test.Service.Managers
{
    public class CryptoManager
    {
        public static string Hash(string text)
        {
            PasswordHasher passwordHasher = new PasswordHasher();
            return passwordHasher.HashPassword(text);
        }

        public static bool Verify(string original, string hashed)
        {
            PasswordHasher passwordHasher = new PasswordHasher();
            return passwordHasher.VerifyHashedPassword(hashed, original)
                != PasswordVerificationResult.Failed;
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            try
            {
                var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
                return Encoding.UTF8.GetString(base64EncodedBytes);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}

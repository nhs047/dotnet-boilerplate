using Localization.Test.Common.Constants;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Localization.Test.Service.Managers
{
    public interface IJwtSettings
    {
        string? SecurityKey { get; set; }
        string? Issuer { get; set; }
        string? Audience { get; set; }
        int ExpiryInDays { get; set; }
    }
    public class JwtSettings : IJwtSettings
    {
        public string? SecurityKey { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public int ExpiryInDays { get; set; }
    }

    public static class JwtTokenManager
    {
        private static IJwtSettings? _jwtSettings;

        public static void Configure(IJwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public static string BuildToken(IEnumerable<Claim> claims)
        {
            var expiry = DateTime.Now.AddDays(Convert.ToInt32(
                _jwtSettings != null ?
                    _jwtSettings.ExpiryInDays :
                    30)
                );
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _jwtSettings != null &&
                _jwtSettings.SecurityKey != null ?
                    _jwtSettings.SecurityKey :
                    string.Empty)
                );
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings != null ?
                    _jwtSettings.Issuer :
                    string.Empty,
                audience: _jwtSettings != null ?
                    _jwtSettings.Audience :
                    string.Empty,
                expires: expiry,
                signingCredentials: creds,
                claims: claims
            );

            // Serializes JWT in Compact Serialization Format.
            var serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return serializedToken;
        }

        public static bool IsValid(string token, out ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal = new ClaimsPrincipal();
            token = token.Replace(Keys.BEARER_WITH_SPACE, string.Empty);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _jwtSettings != null &&
                _jwtSettings.SecurityKey != null ?
                    _jwtSettings.SecurityKey :
                    string.Empty)
                );
            var validationParameters = new TokenValidationParameters()
            {
                ValidIssuer = _jwtSettings != null ?
                    _jwtSettings.Issuer :
                    string.Empty,
                ValidAudience = _jwtSettings != null ?
                    _jwtSettings.Audience :
                    string.Empty,
                IssuerSigningKey = key,
                ValidateIssuerSigningKey = true,
                ValidateAudience = true,
                RequireExpirationTime = true
            };

            var validator = new JwtSecurityTokenHandler();
            if (!validator.CanReadToken(token)) return false;
            try
            {
                // This line throws if invalid
                claimsPrincipal = validator.ValidateToken(token, validationParameters, out _);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

    }
}

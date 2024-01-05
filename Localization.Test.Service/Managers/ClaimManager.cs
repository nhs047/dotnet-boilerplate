using Localization.Test.Common.Models;
using System.Reflection;
using System.Security.Claims;

namespace Localization.Test.Service.Managers
{
    public class ClaimManager
    {
        public static List<Claim> PrepareClaims(UserParams userParams, List<string> roles)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userParams.Email ?? string.Empty)
            };

            Type type = userParams.GetType();
            PropertyInfo[] props = type.GetProperties();
            foreach (var prop in props)
            {
                claims.Add(new Claim(prop.Name, prop.GetValue(userParams)?.ToString() ?? string.Empty));
            }

            foreach (var role in roles)
            {
                claims.Add(new Claim("scope", role));
            }

            return claims;
        }
    }
}

using Microsoft.AspNetCore.Http;

namespace Localization.Test.Common.Helpers
{
    public class IdentityResolver
    {
        private readonly IHttpContextAccessor _context;
        public IdentityResolver(IHttpContextAccessor context)
        {
            _context = context;
        }
        public string? Firstname => GetClaim("Firstname");

        public string? Lastname => GetClaim("Lastname");

        public string? UserId => GetClaim("UserId");

        public string? Email => GetClaim("Email");
        public string? Culture => GetClaim("Culture");

        public List<string> Scopes => GetScopesClaim();

        private string? GetClaim(string claimName)
        {
            var claim = _context.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == claimName);
            return claim?.Value;
        }

        private List<string> GetScopesClaim()
        {
            List<string> scopes = new List<string>();

            var claims = _context.HttpContext?.User.Claims.Where(x => x.Type == "scope").ToList();

            if (claims == null) return scopes;

            foreach (var claim in claims)
            {
                scopes.Add(claim.Value);
            }

            return scopes;
        }
    }
}

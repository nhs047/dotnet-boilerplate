using Localization.Test.API.Helpers;
using Localization.Test.Common.Constants;
using Localization.Test.Common.Helpers;
using System.Globalization;

namespace Localization.Test.API.Middlewares
{
    public class CultureMiddleware
    {
        private readonly RequestDelegate _request;
        public CultureMiddleware(
           RequestDelegate requestDelegate)
        {
            _request = requestDelegate ?? throw new ArgumentNullException(
                    nameof(requestDelegate),
                    nameof(requestDelegate) + Keys.IS_REQUIRED);
        }

        public async Task InvokeAsync(HttpContext context, IdentityResolver identityResolver)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context), nameof(context) + Keys.IS_REQUIRED);
            }
            //var culture = identityResolver.Culture ?? context.Request.Query["culture"].ToString();
            var ttt = context.Request.Query["culture"].ToString();
            Helper.SetCulture(identityResolver.Culture ?? Keys.DefaultCulture);
            await _request(context);
        }

    }
}
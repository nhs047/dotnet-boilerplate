using Localization.Test.API.Helpers;
using Localization.Test.Common.Constants;
using Localization.Test.Service;
using System.Net;

namespace Localization.Test.API.Middlewares
{
    public class RequestResponseMiddleware
    {
        private readonly RequestDelegate _request;
        public RequestResponseMiddleware(RequestDelegate requestDelegate)
        {
            _request = requestDelegate ?? throw new ArgumentNullException(
                    nameof(requestDelegate),
                    nameof(requestDelegate) + Keys.IS_REQUIRED);
        }
        public async Task InvokeAsync(HttpContext context, LanguageService languageService)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context), nameof(context) + Keys.IS_REQUIRED);
            }

            var headerTokenExtracted = context.Request.Headers.TryGetValue(Keys.TOKEN_HEADER, out var headerToken);
            if (headerTokenExtracted)
            {
                context.Request.Headers.Add(Keys.AUTHORIZATION_HEADER, headerToken);
            }

            await _request(context);

            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                ResponseHelper.WriteUnauthorizedResponse(context, languageService.GetString(MessageCodes.TOKEN_REQUIRED));
            }

            if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
            {

                ResponseHelper.WriteForbiddenResponse(context, languageService.GetString(MessageCodes.NOT_AUTHORIZED));
            }

            if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
            {
                ResponseHelper.WriteErrorResponse(context, languageService.GetString(MessageCodes.NOT_FOUND));
            }

            if (context.Response.StatusCode == (int)HttpStatusCode.UnsupportedMediaType)
            {
                ResponseHelper.WriteErrorResponse(context, languageService.GetString(MessageCodes.UNSUPPORTED_MEDIA));
            }
        }

    }
}

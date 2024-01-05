
using Localization.Test.API.Helpers;
using Localization.Test.Common.Constants;
using Localization.Test.Common.Exceptions;

namespace Localization.Test.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _request;

        public ExceptionMiddleware(
            RequestDelegate requestDelegate)
        {
            _request = requestDelegate ?? throw new ArgumentNullException(
                    nameof(requestDelegate),
                    nameof(requestDelegate) + Keys.IS_REQUIRED);
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context), nameof(context) + Keys.IS_REQUIRED);
            }

            try
            {
                await _request(context);
            }
            catch (ErrorOutException eoEx)
            {
                ResponseHelper.WriteErrorResponse(context, eoEx.Message);
            }
            catch (FileNotFoundException fnfe)
            {
                ResponseHelper.WriteErrorResponse(context, fnfe.Message);
            }
            catch (Exception ex)
            {
                ResponseHelper.WriteErrorResponse(context, ex.Message);
            }
        }
    }
}

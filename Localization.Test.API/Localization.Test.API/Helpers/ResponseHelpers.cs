using Localization.Test.Service.Extensions;
using Newtonsoft.Json;
using System.Net;

namespace Localization.Test.API.Helpers
{
    public static class ResponseHelper
    {
        public static void WriteErrorResponse(HttpContext context, string message)
        {
            var responseString = JsonConvert.SerializeObject(ResponseObject<object>.Build().setIsError(true).setMessage(message));
            context.WriteResponse(HttpStatusCode.BadRequest, responseString);
        }

        public static void WriteUnauthorizedResponse(HttpContext context, string message)
        {
            var responseString = JsonConvert.SerializeObject(ResponseObject<object>.Build().setIsError(true).setMessage(message));
            context.WriteResponse(HttpStatusCode.Unauthorized, responseString);
        }

        public static void WriteForbiddenResponse(HttpContext context, string message)
        {
            var responseString = JsonConvert.SerializeObject(ResponseObject<object>.Build().setIsError(true).setMessage(message));
            context.WriteResponse(HttpStatusCode.Forbidden, responseString);
        }
    }
}

using Microsoft.AspNetCore.Http;
using System.Net;

namespace Localization.Test.Service.Extensions
{
    public static class HttpContextExtention
    {
        public static void WriteResponse(this HttpContext context, HttpStatusCode statusCode)
        {
            if (!context.Response.HasStarted)
            {
                context.Response.StatusCode = (int)statusCode;
            }
        }
 
        public static void WriteResponse(this HttpContext context, HttpStatusCode statusCode, string response)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            context.Response.WriteAsync(response);
        }
    }
}

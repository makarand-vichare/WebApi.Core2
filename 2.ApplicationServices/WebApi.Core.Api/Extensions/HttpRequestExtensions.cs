using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApi.Core.Extensions
{
    public static class HttpRequestExtensions
    {
        public static IActionResult CreateResponse(this HttpRequest request, HttpStatusCode status, object content)
        {
            return new ObjectResult(content)
            {
                StatusCode = (int)status
            };
        }

        public static IActionResult CreateErrorResponse(this HttpRequest request, object content)
        {
            return new ObjectResult(content)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }
    }
}

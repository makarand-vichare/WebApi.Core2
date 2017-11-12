using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;

namespace WebApi.Core.Controllers
{
    [Route("api/Antiforgerytoken")]
    public class AntiForgeryTokenController : BaseController
    {

        [HttpGet]
        [Route("GetAntiForgeryToken")]
        public HttpResponseMessage GetAntiForgeryToken()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            //HttpCookie cookie = HttpContext.Request.Cookies[AppConstants.XsrfCookie];
            //string oldCookieToken = cookie == null ? "" : cookie.Value;
            //string cookieToken;
            //string formToken;
            //AntiForgery.GetTokens(oldCookieToken, out cookieToken, out formToken);

            //var content = new { FormToken = formToken , CookieToken = cookieToken };

            //response.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            //if (!string.IsNullOrEmpty(cookieToken))
            //{
            //    CookieHeaderValue cookieData = new CookieHeaderValue(AppConstants.XsrfCookie, cookieToken);
            //    cookieData.Expires = DateTimeOffset.Now.AddMinutes(10);
            //    cookieData.Domain = Request.RequestUri.Host;
            //    cookieData.Path = "/";
            //    response.Headers.AddCookies(new CookieHeaderValue[] { cookieData });
            //}

            return response;
        }
    }
}

using WebApi2.IDomainServices.IdentityStores;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using WebApi2.Utility;

namespace WebApi2.EndPointApi.Controllers
{
    [RoutePrefix("api/RefreshTokens")]
    public class RefreshTokensController : ApiController
    {

        private readonly IRefreshTokenService refreshTokenService;

        public RefreshTokensController(IRefreshTokenService refreshTokenService)
        {
            this.refreshTokenService = refreshTokenService;
        }

        [Authorize(Users = "Admin")]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(refreshTokenService.GetAllRefreshTokens());
        }

        //[Authorize(Users = "Admin")]
        [AllowAnonymous]
        [Route("")]
        public async Task<IHttpActionResult> Delete(string tokenId)
        {
            var result = await refreshTokenService.RemoveRefreshToken(tokenId);
            if (result)
            {
                return Ok();
            }
            return BadRequest("Token Id does not exist");

        }

        [HttpGet]
        [Route("antiforgerytoken")]
        public HttpResponseMessage GetAntiForgeryToken()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            HttpCookie cookie = HttpContext.Current.Request.Cookies[AppConstants.XsrfCookie];

            string cookieToken;
            string formToken;
            AntiForgery.GetTokens(cookie == null ? "" : cookie.Value, out cookieToken, out formToken);

            var content = new {  AntiForgeryToken = formToken };

            response.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            if (!string.IsNullOrEmpty(cookieToken))
            {
                response.Headers.AddCookies(new[]
                {
                    new CookieHeaderValue(AppConstants.XsrfCookie, cookieToken)
                    {
                        Expires = DateTimeOffset.Now.AddMinutes(10),
                        Path = "/"
                    }
                });
            }

            return response;
        }
    }
}

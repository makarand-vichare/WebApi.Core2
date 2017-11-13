using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebApi.Core.IDomainServices.IdentityStores;

namespace WebApi.Core.Controllers
{
    [Route("api/[controller]")]
    public class RefreshTokensController : BaseController
    {

        private readonly IRefreshTokenService refreshTokenService;

        public RefreshTokensController(IRefreshTokenService refreshTokenService)
        {
            this.refreshTokenService = refreshTokenService;
        }

        [Authorize(Roles = "Admin")]
        [Route("antiforgerytokens")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(refreshTokenService.GetAllRefreshTokens());
        }

        [Authorize(Roles = "Admin")]
        [AllowAnonymous]
        [HttpDelete]
        public async Task<IActionResult> Delete(string tokenId)
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
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            //HttpCookie cookie = HttpContext.Current.Request.Cookies[AppConstants.XsrfCookie];

            //string cookieToken;
            //string formToken;
            //AntiForgery.GetTokens(cookie == null ? "" : cookie.Value, out cookieToken, out formToken);

            //var content = new {  AntiForgeryToken = formToken };

            //response.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            //if (!string.IsNullOrEmpty(cookieToken))
            //{
            //    response.Headers.AddCookies(new[]
            //    {
            //        new CookieHeaderValue(AppConstants.XsrfCookie, cookieToken)
            //        {
            //            Expires = DateTimeOffset.Now.AddMinutes(10),
            //            Path = "/"
            //        }
            //    });
            //}

            return response;
        }
    }
}

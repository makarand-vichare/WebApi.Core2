using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi2.IDomainServices.Services;

namespace WebApi2.EndPointApi.Controllers
{
    [RoutePrefix("api/Localizations")]
    public class LocalizationController : ApiController
    {
        private readonly ILocalizationService localizationService;

        public LocalizationController(ILocalizationService localizationService)
        {
            this.localizationService = localizationService;
        }

        [Route("GetResourceKeys")]
        [HttpGet]
        public HttpResponseMessage GetResourceKeys(string keyGroup,string languageCode)
        {
            try
            {
                var resourceKeys = localizationService.GetLocalizations(keyGroup,languageCode);
                return Request.CreateResponse(HttpStatusCode.OK, resourceKeys);
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}

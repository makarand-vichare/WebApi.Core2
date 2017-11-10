using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi2.IDomainServices.Services;

namespace WebApi2.EndPointApi.Controllers
{
    [RoutePrefix("api/Country")]
    public class CountryController : ApiController
    {
        private readonly ICountryService countryService;

        public CountryController(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        [Route("GetCountries")]
        [HttpGet]
        public HttpResponseMessage GetCountries()
        {
            try
            {
                var lookupList = countryService.GetLookup();
                return Request.CreateResponse(HttpStatusCode.OK, lookupList);
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}

using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi2.IDomainServices.Services;

namespace WebApi2.EndPointApi.Controllers
{
    [RoutePrefix("api/City")]
    public class CityController : ApiController
    {
        private readonly ICityService cityService;

        public CityController(ICityService cityService)
        {
            this.cityService = cityService;
        }

        [Route("GetCities")]
        [HttpGet]
        public HttpResponseMessage GetCities(long countryId)
        {
            try
            {
                var lookupList = cityService.GetLookup(countryId);
                return Request.CreateResponse(HttpStatusCode.OK, lookupList);
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}

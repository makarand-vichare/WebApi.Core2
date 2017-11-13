using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using WebApi.Core.Extensions;
using WebApi.Core.IDomainServices.Services;

namespace WebApi.Core.Controllers
{
    [Route("api/[controller]")]
    public class CityController : BaseController
    {
        private readonly ICityService cityService;

        public CityController(ICityService cityService)
        {
            this.cityService = cityService;
        }

        [Route("GetCities")]
        [HttpGet]
        public IActionResult GetCities(long countryId)
        {
            try
            {
                var lookupList = cityService.GetLookup(countryId);
                return Request.CreateResponse(HttpStatusCode.OK, lookupList);
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(ex);
            }
        }
    }
}

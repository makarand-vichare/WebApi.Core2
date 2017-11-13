using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using WebApi.Core.Extensions;
using WebApi.Core.IDomainServices.Services;

namespace WebApi.Core.Controllers
{
    [Route("api/[controller]")]
    public class CountryController : BaseController
    {
        private readonly ICountryService countryService;

        public CountryController(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        [Route("GetCountries")]
        [HttpGet]
        public IActionResult GetCountries()
        {
            try
            {
                var lookupList = countryService.GetLookup();
                return Request.CreateResponse(HttpStatusCode.OK, lookupList);
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(ex);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using WebApi.Core.Extensions;
using WebApi.Core.IDomainServices.Services;

namespace WebApi.Core.Controllers
{
    [Route("api/[controller]")]
    public class LocalizationController : BaseController
    {
        private readonly ILocalizationService localizationService;

        public LocalizationController(ILocalizationService localizationService)
        {
            this.localizationService = localizationService;
        }

        [Route("GetResourceKeys")]
        [HttpGet]
        public IActionResult GetResourceKeys(string keyGroup,string languageCode)
        {
            try
            {
                var resourceKeys = localizationService.GetLocalizations(keyGroup,languageCode);
                return Request.CreateResponse(HttpStatusCode.OK, resourceKeys);
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse( ex);
            }
        }
    }
}

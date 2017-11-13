using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StructureMap.Attributes;

namespace WebApi.Core.Controllers
{
    //[Produces("application/json")]
    [Route("api/Base")]
    public abstract class BaseController : ControllerBase
    {
        [SetterProperty]
        public ILogger AppLogger { get; set; }

    }
}
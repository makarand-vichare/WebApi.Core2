using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi2.ViewModels.Identity.WebApi;

namespace WebApi2.EndPointApi.Controllers
{
    //[Authorize]
    public class TestApiController : ApiController
    {
        // GET api/values
        public HttpResponseMessage Get()
        {
            var result = new List<IdentityUserViewModel>() {
                new IdentityUserViewModel { Id = 1 , UserName ="User1" },
                 new IdentityUserViewModel { Id = 2 , UserName ="User2" },
                new IdentityUserViewModel { Id = 3 , UserName ="User3"},
                new IdentityUserViewModel { Id = 4 , UserName ="User4" }
           };
            if (result.Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { result, message = "found records" });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No Record Found");
            }
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}

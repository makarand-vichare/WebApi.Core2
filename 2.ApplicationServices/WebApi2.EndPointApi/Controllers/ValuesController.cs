using WebApi2.ViewModels.Identity.WebApi;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi2.EndPointApi.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        public HttpResponseMessage Get()
        {
            var result = new List<IdentityUserViewModel>() {
                new IdentityUserViewModel { Id = 1 , Email ="email1@mail.com", DateOfBirth = DateTime.Now.AddYears(-20), Gender = 1  },
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

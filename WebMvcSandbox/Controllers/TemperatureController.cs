using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebMvcSandbox.Models;
using WebMvcSandbox.Services;

namespace WebMvcSandbox.Controllers
{
    public class TemperatureController : ApiController
    {
        // GET
        public IEnumerable<TemperatureEntry> Get()
        {
            return TemperatureService.GetTempEntries();
        }

        // POST
        [HttpPost]
        public HttpResponseMessage Post([FromBody]Double value)
        {
            TemperatureService.AddTempEntry(value);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
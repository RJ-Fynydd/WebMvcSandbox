using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebMvcSandbox.Models;
using WebMvcSandbox.Services;

namespace WebMvcSandbox.Controllers
{
    public class TestController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<TemperatureEntry> Get()
        {
            return EntryService.GetTempEntries();
        }

        // POST api/<controller>/i
        [HttpPost]
        public HttpResponseMessage Post([FromBody]Double value)
        {
            System.Diagnostics.Debug.WriteLine("Reported Temperature: " + value);

            EntryService.AddTempEntry(value);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
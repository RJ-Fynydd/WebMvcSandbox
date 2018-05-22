using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebMvcSandbox.Services;

namespace WebMvcSandbox.Controllers
{
    public class TestController : ApiController
    {
        // GET api/<controller>
        public string Get()
        {
            return DateTime.Now.ToString();
        }

        // POST api/<controller>/i
        [HttpPost]
        public HttpResponseMessage Post([FromBody]string value)
        {
            System.Diagnostics.Debug.WriteLine(value);

            EntryService.AddTempEntry(value);
            //EntryService.AddEntry(new Models.Entry(Convert.ToString(value), "From the arduino."));

            return new HttpResponseMessage(HttpStatusCode.Created);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NLog;
using EchoWeb.Speechlets;


namespace EchoWeb.Controllers
{
    public class UltronController : ApiController
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        // GET api/ultron
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/ultron/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/ultron
        public HttpResponseMessage Post()
        {
            var speechlet = new UltronSpeechlet();
            var response = speechlet.GetResponse(Request);

            return response;
        }

        // PUT api/ultron/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/ultron/5
        public void Delete(int id)
        {
        }
    }
}

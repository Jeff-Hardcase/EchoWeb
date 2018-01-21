using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NLog;
using EchoWeb.Models;

namespace EchoWeb.Controllers
{
    public class AlexaController : ApiController
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        // GET api/alexa
        public IEnumerable<string> Get()
        {
            log.Info("testing nlog");

            return new string[] { "Testing the HTTP GET", "got it" };
        }

        // GET api/alexa/5
        public string Get(int id)
        {
            return "parameter value is = " + id;
        }

        // POST api/alexa
        [HttpPost]
        public Boolean Post(testModel model)
        {
            log.Info("POST - value: " + model.testValue + ", key: " + model.testKey);

            return true;
        }

        // PUT api/alexa/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/alexa/5
        public void Delete(int id)
        {
        }
    }
}

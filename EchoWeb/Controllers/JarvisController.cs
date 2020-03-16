using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NLog;
using EchoWeb.Models.Google;
using EchoWeb.Models.Repositories;

namespace EchoWeb.Controllers
{
    public class JarvisController : ApiController
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        // GET: api/Jarvis
        public string Get()
        {
            return "testing the Google Home";
        }

        // GET: api/Jarvis/5
        public string Get(int id)
        {
            return "value is " + id.ToString();
        }

        // POST: api/Jarvis
        public HttpResponseMessage Post([FromBody]DeviceRequest request)
        {
            var _repo = new JarvisRepository();
            var response = _repo.SetYamahaInput(request);
            
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

    }
}

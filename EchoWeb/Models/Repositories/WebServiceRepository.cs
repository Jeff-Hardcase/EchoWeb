using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using NLog;

namespace EchoWeb.Models.Repositories
{
    public enum HttpVerb
    {
        Get,
        Post,
        Put,
        Delete
    };

    public enum ServiceDataType
    {
        Json,
        XML
    };

    public class WebServiceRepository
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public string wsCallGetJSON(string wsURL, HttpVerb verb, object data, Guid? cslToken = null)
        {
            var result = string.Empty;

            var jsonData = wsCall(wsURL, verb, data, cslToken);

            if (jsonData.IsSuccessStatusCode)
            {
                result = jsonData.Content.ReadAsStringAsync().Result;
            }
            else
            {
                logger.Info(string.Format("ws call NOT successful. HTTP verb - {0}, to {1}", verb.ToString(), wsURL));
            }

            return result;
        }

        public HttpResponseMessage wsCall(string wsURL, HttpVerb verb, object data, Guid? cslToken = null)
        {
            //assumes json in and out
            var response = wsCall(wsURL, verb, data, ServiceDataType.Json, cslToken);
            
            return response;
        }

        public HttpResponseMessage wsCall(string wsURL, HttpVerb verb, object data, ServiceDataType serializeTo = ServiceDataType.Json, Guid? cslToken = null)
        {
            var jsonData = new HttpResponseMessage();
            HttpContent _content = null;

            //log info if needed
            if (Properties.Settings.Default.LogWsCalls)
            {
                var jsonContent = (data != null) ? ServiceStack.Text.JsonSerializer.SerializeToString(data) : "";

                logger.Info(string.Format("HTTP verb - {0}, to {1}, data : {2}", verb.ToString(), wsURL, jsonContent));
            }

            using (var client = new HttpClient())
            {
                //assume json return, possible allow others later
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                if (cslToken.HasValue)
                {
                    client.DefaultRequestHeaders.Add("Authorization", cslToken.Value.ToString());
                }

                var _uri = new Uri(wsURL);

                if (data != null && verb != HttpVerb.Get)
                {
                    if (serializeTo == ServiceDataType.XML)
                        _content = new StringContent(data.ToString(), System.Text.Encoding.UTF8, "text/xml");
                    else
                        _content = new ObjectContent(data.GetType(), data, new System.Net.Http.Formatting.JsonMediaTypeFormatter());
                }

                switch (verb)
                {
                    case HttpVerb.Get:
                        jsonData = client.GetAsync(_uri).Result;
                        break;
                    case HttpVerb.Post:
                        jsonData = client.PostAsync(_uri, _content).Result;
                        break;
                    case HttpVerb.Put:
                        jsonData = client.PutAsync(_uri, _content).Result;
                        break;
                    case HttpVerb.Delete:
                        jsonData = client.DeleteAsync(_uri).Result;
                        break;
                    default:
                        jsonData = client.GetAsync(_uri).Result;
                        break;
                }
            }

            return jsonData;
        }
    }
}
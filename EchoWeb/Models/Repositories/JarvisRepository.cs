using System.Collections.Generic;
using NLog;
using EchoWeb.Models.Google;

namespace EchoWeb.Models.Repositories
{
    public class JarvisRepository
    {
        public DeviceResponse SetYamahaInput(DeviceRequest request)
        {
            var _yamaha = new YamahaAV_Repository();
            var response = new DeviceResponse();

            response.fulfillmentText = "Jarvisbot response from EchoWeb";

            var target = request.queryResult.parameters.target;
            var success = _yamaha.SetInput(target);
            
            if(success)
            {
                var speak = new SimpleResponse { textToSpeech = "Jarvis set Yamaha to " + target };
                var item = new Item { simpleResponse = speak };
                var list = new List<Item>();

                list.Add(item);

                response.payload = new Payload { google = new GoogleResponse { expectUserResponse = false, richResponse = new RichResponse { items = list } } };
            }

            return response;
        }

    }
}
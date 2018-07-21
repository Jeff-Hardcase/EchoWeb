using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EchoWeb.Models
{
    public class SpeechResponseModel
    {
        public string responseTitle { get; set; }
        public string responseOutput { get; set; }
        public bool responseSuccess { get; set; }
        public bool shouldEndSession { get; set; }

        public SpeechResponseModel()
        {
            responseTitle = "Ultron Response to Unknown Command";
            responseOutput = "Your inferior mind has failed you again. Please reformulate your request.";
        }
    }
}
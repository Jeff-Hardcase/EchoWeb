using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlexaSkillsKit.Slu;

namespace EchoWeb.Models.Repositories
{
    public class FlightRepository
    {
        public SpeechResponseModel FlightSearch(Intent intent)
        {
            var response = new SpeechResponseModel();
            var rnd = new Random();

            var fromCity = "Houston";
            var toCity = "Seattle";
            var spokenText = "United Flight {0} from {1} to {2} leaves at {3} tomorrow. Would you like to purchase a seat on this flight?";

            if (intent.Slots.Count > 0)
            {
                fromCity = intent.Slots["FromCity"].Value;
                toCity = intent.Slots["ToCity"].Value;
            }

            var flightTime = DateTime.Today.AddMinutes(300 + rnd.Next(1020));

            response.responseOutput = string.Format(spokenText, rnd.Next(23, 1997), fromCity, toCity, flightTime.ToShortTimeString());
            response.responseSuccess = true;
            
            return response;
        }
    }
}
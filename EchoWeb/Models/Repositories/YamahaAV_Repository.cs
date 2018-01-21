using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using AlexaSkillsKit.Slu;


namespace EchoWeb.Models.Repositories
{
    public enum YamahaInputs
    {
        Cable,
        Chromecast,
        Computer,
        PC,
        FireTV,
        Amazon,
        Apple,
        Playstation,
        Console,
        Radio,
        Bluetooth
    }

    public class YamahaAV_Repository
    {
        private WebServiceRepository _wsRepo = new WebServiceRepository();

        public bool SwitchInput(Intent intent)
        {
            var result = false;
            var caseIgnore = true;
            YamahaInputs avInput;

            if (Enum.TryParse(intent.Slots["Input"].Value, caseIgnore, out avInput))
                result = SwitchInput(avInput);

            return result;
        }

        public bool SwitchInput(YamahaInputs input)
        {
            var status = false;
            var inputSwitch = "<YAMAHA_AV cmd=\"PUT\" ><Main_Zone><Input><Input_Sel>{0}</Input_Sel></Input></Main_Zone></YAMAHA_AV>";
            var inputLabel = string.Empty;
            var inputCommand = string.Empty;

            switch (input)
            {
                case YamahaInputs.Cable:
                    inputLabel = "HDMI2";
                    break;
                case YamahaInputs.Computer:
                case YamahaInputs.PC:
                    inputLabel = "HDMI3";
                    break;
                case YamahaInputs.FireTV:
                case YamahaInputs.Amazon:
                    inputLabel = "HDMI5";
                    break;
                case YamahaInputs.Chromecast:
                    inputLabel = "HDMI6";
                    break;
                case YamahaInputs.Apple:
                    inputLabel = "HDMI4";
                    break;
                case YamahaInputs.Playstation:
                case YamahaInputs.Console:
                    inputLabel = "HDMI1";
                    break;
                default:
                    return false;
            }

            inputCommand = string.Format(inputSwitch, inputLabel);

            //POST this command to the Yamaha URL
            status = wsPost(inputCommand);

            return status;
        }

        private bool wsPost(string xmlData)
        {
            var yamahaURL = Properties.Settings.Default.YamahaURL;
            var response = _wsRepo.wsCall(yamahaURL, HttpVerb.Post, xmlData, ServiceDataType.XML);

            return response.IsSuccessStatusCode;
        }
    }
}
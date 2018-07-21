using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EchoWeb.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public string TestFile()
        {
            var fileFolder = @"P:\Jeff\data\clientBot\";
            var displayName = @"LivingRoom\";
            var fileName = "DashboardSummary.txt";
            var filePath = fileFolder + displayName + fileName;
            
            System.IO.FileStream fs = null;

            if (!System.IO.File.Exists(filePath))
            {
                using (fs = System.IO.File.Create(filePath))
                {

                }
            }

            return "created file - " + filePath;
        }

        public string TestCode()
        {
            var _repo = new EchoWeb.Models.Repositories.FlightRepository();
            
            var intent = new AlexaSkillsKit.Slu.Intent();
            var origin = new AlexaSkillsKit.Slu.Slot();
            var dest = new AlexaSkillsKit.Slu.Slot();

            intent.Slots = new Dictionary<string, AlexaSkillsKit.Slu.Slot>();

            origin.Name = "FromCity";
            origin.Value = "New York";

            dest.Name = "ToCity";
            dest.Value = "Houston";

            intent.Slots.Add(origin.Name, origin);
            intent.Slots.Add(dest.Name, dest);

            var response = _repo.FlightSearch(intent);

            return response.responseOutput;
        }

    }
}

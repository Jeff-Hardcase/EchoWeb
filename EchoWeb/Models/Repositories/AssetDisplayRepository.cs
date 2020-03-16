using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlexaSkillsKit.Slu;
using EchoWeb.Extensions;

namespace EchoWeb.Models.Repositories
{
    public class AssetDisplayRepository
    {
        private YamahaAV_Repository yamaha = new YamahaAV_Repository();

        public bool ShowAssets(Intent intent)
        {
            var result = false;
            var yamahaResult = true;
            var location = "LivingRoom";
            var slot = intent.Slots.First();

            if (slot.Value.Value != null)
                location = slot.Value.Value.ToTitleCase().Replace(" ", "");

            result = CreateCommandFile(location);

            if (location == "LivingRoom" || location == "Frankenstein")
                yamahaResult = yamaha.SetInput(YamahaInputs.Computer);

            return result && yamahaResult;
        }

        private bool CreateCommandFile(string displayLocation)
        {
            var result = false;

            var fileFolder = @"P:\Jeff\data\clientBot\";
            var fileName = "DashboardSummary.txt";

            var filePath = System.IO.Path.Combine(fileFolder, displayLocation, fileName);

            System.IO.FileStream fs = null;

            if (!System.IO.File.Exists(filePath))
            {
                using (fs = System.IO.File.Create(filePath))
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
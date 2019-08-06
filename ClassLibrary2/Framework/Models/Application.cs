using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationFramework.Framework.Enums;
using AutomationFramework.Framework.Helpers;

namespace AutomationFramework.Framework.Models
{
    public class Application
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public AppCategory Category { get; set; }


        public static Application GenerateNewApplication()
        {
            var newApplication = new Application();

            newApplication.Title = $"NewApp_{RandomHelper.CreateRandomAlphaNumeric(6)}";
            newApplication.Description = $"Description_{RandomHelper.CreateRandomAlphaNumeric(10)}";
            newApplication.Category = RandomHelper.GetUniqueEnumValue<AppCategory>();

            return newApplication;
        }

    }
}

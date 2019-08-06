using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationFramework.Components.Pages;

namespace AutomationFramework.Components
{
    public class SiteNavigator : BasePage
    {
        public static void GoToAjaxTestPage() => 
            DriverImplementation.Driver.Navigate().GoToUrl(ConfigurationManager.AppSettings.Get("ajaxTestPage"));

        public static void GoToJsTestPage() => DriverImplementation.Driver.Navigate()
            .GoToUrl(ConfigurationManager.AppSettings.Get("jsTestPage"));

        public static void GoToRegistrationForm() =>
            DriverImplementation.Driver.Navigate().GoToUrl(ConfigurationManager.AppSettings.Get("registerURL"));

        public static void GoToMyAppsPage() =>
            DriverImplementation.Driver.Navigate().GoToUrl(ConfigurationManager.AppSettings.Get("myAppsURL"));
        public static void GoToNewAppPage() =>
            DriverImplementation.Driver.Navigate().GoToUrl(ConfigurationManager.AppSettings.Get("newAppURL"));
        public static void GoToHomePage() => DriverImplementation.Driver.Navigate().GoToUrl(ConfigurationManager.AppSettings.Get("basicUrl"));
        public static void GoToBasicAuthLoginPage(string login, string password) => DriverImplementation.Driver.Navigate().GoToUrl($"http://{login}:{password}@192.168.197.61:8081/");



    }
}

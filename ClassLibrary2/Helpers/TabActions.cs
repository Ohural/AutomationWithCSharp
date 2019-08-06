using OpenQA.Selenium;

namespace AutomationFramework.Framework.Helpers
{
    public class TabActions
    {
        
        public static void OpenUrlAndSwitchToNewTab(string url)
        {
            ((IJavaScriptExecutor)DriverImplementation.Driver).ExecuteScript("window.open();");
            DriverImplementation.Driver.SwitchTo().Window(DriverImplementation.Driver.WindowHandles[1]);
            DriverImplementation.Driver.Navigate().GoToUrl(url);
        }

        public static void CloseNewTabAndSwitchToPrevious()
        {
            DriverImplementation.Driver.SwitchTo().Window(DriverImplementation.Driver.WindowHandles[1]).Close();
            DriverImplementation.Driver.SwitchTo().Window(DriverImplementation.Driver.WindowHandles[0]);
        }
    }
}

using AutomationFramework.Components.Panels;
using AutomationFramework.Framework.Helpers;
using OpenQA.Selenium;

namespace AutomationFramework.Components.Pages
{
    public class HomePage : BasePage
    {
        public HeaderPanel HeaderPanel => new HeaderPanel();

        public void SelectRandomApp()
        {
           var position = RandomHelper.CreateRandomNumber(10);
           var randomApp = DriverImplementation.Driver.FindElement(By.XPath($"//div[@class = 'app'][{position}]//a[contains(text(),'Details')]"));
           randomApp.Click();

        }

        public bool CheckAppIsPopular(string appName)
        {
            if (IsElementVisible(By.XPath($"//div[@class = 'popular-app']//div[text() = '{appName}']")))
            {
                return true;
            }

            return false;
        }

        public void ClickOnPopularApp(string appName)
        {
            var element = DriverImplementation.Driver.FindElement(
                By.XPath($"//div[@class = 'popular-app']//div[text() = '{appName}']"));
            element.Click();
        }
    }

   
}

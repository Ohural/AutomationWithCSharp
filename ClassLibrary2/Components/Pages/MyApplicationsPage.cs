using AutomationFramework.Components.Panels;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace AutomationFramework.Components.Pages
{
    public class MyApplicationsPage : BasePage
    {
        public HeaderPanel HeaderPanel => new HeaderPanel();

        [FindsBy(How = How.LinkText, Using = "Click to add new application")]
        private IWebElement AddNewAppLink;


        public void ClickAddNewApp()
        {
            AddNewAppLink.Click();
        }

        public bool IsAddNewAppVisible => AddNewAppLink.Displayed;

        public bool CheckNewAppPresence(string element)
        {
            var elementName = By.XPath($"//div[@class = 'name' and (contains(text(),'{element}'))]");
            var state = IsElementVisible(elementName);

            return state;
        }

        public void GoToAppDetails(string appName)
        {
            var element = DriverImplementation.Driver.FindElement(By.XPath($"//a[contains(@href,'{appName}')]"));
            element.Click();
            
        }
    }
}

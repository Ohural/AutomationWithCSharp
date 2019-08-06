using AutomationFramework.Components.Pages;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace AutomationFramework.Components.Panels
{
    public class HeaderPanel : BasePage
    {
        [FindsBy(How = How.ClassName, Using = "welcome")]
        protected IWebElement WelcomeLabel { get; set; }

        [FindsBy(How = How.LinkText, Using = "Home")]
        protected IWebElement HomeLink { get; set; }

        [FindsBy(How = How.LinkText, Using = "My applications")]
        protected IWebElement MyAppsLink { get; set; }

        [FindsBy(How = How.LinkText, Using = "Logout")]
        protected IWebElement LogOutLink { get; set; }

        public string WelcomeText => WelcomeLabel.Text;

        public void Logout()
        {
            LogOutLink.Click();
            
        }
        
        public void ClickOnMyAppsButton()
        {
            MyAppsLink.Click();
        }

        
        public bool CheckIsMyAppsVisible()
        {
            var status = IsElementVisible(By.LinkText("My applications"));
            return status;

        } 
    }
}

using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace AutomationFramework.Components.Pages
{
    public class LoginPage : BasePage
    {
        
        [FindsBy(How = How.Id, Using = "j_username")]
        private IWebElement UsernameBox { get; set; }

        [FindsBy(How = How.Id, Using = "j_password")]
        private IWebElement PasswordBox { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@value='Login']")]
        private IWebElement LoginButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[contains(@href,'register')]")]
        private IWebElement RegisterLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//h1[text() = 'Login']")]
        private IWebElement LoginSign { get; set; }

        public void Login(string login, string password)
        {
            if (IsLoginButtonVisible().Equals(true))
            {
                UsernameBox.SendKeys(login);
                PasswordBox.SendKeys(password);
                LoginButton.Click();
            }
            else
            {
                DriverImplementation.Driver.SwitchTo().Alert().SetAuthenticationCredentials(login, password);
                DriverImplementation.Driver.SwitchTo().Alert().Accept();
            }
        }

        public void LoginWithBasicAuth(string login, string password)
        {
            SiteNavigator.GoToBasicAuthLoginPage(login,password);
        }
        

        public void RegisterLinkClick()
        {
            RegisterLink.Click();

        }

        public string LoginSignText => LoginSign.Text;

        public bool IsLoginButtonVisible() => LoginButton.Displayed;
    }
}

using System;
using AutomationFramework.Components.Panels;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;


namespace AutomationFramework.Components.Pages
{
    public class BasePage
    {
        protected IWebDriver WebDriver;

        [FindsBy(How = How.CssSelector, Using = ".flash")]
        protected IWebElement FlashMessage { get; set; }

        protected BasePage()
        {
            this.WebDriver = DriverImplementation.Driver;
            PageFactory.InitElements(WebDriver, this);
        }
        
        public String GetFlashMessage()
        {
            WaitForElementIsVisible(2,By.CssSelector(".flash"));
            return FlashMessage.Text;
        }
        public bool IsEmpty(string element)
        {
            if (element != null)
            {
                return false;
            }

            return true;

        }

        public void AcceptAlert()
        {
            DriverImplementation.Driver.SwitchTo().Alert().Accept();
        }

        public void WaitForElementIsVisible(int seconds, By by)
        {
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(seconds));
            wait.Until(ExpectedConditions.ElementIsVisible(by));
        }

        public void WaitForElementIsInvisible(int seconds, By by)
        {
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(seconds));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(by));
        }

        public void WaitForAjax()
        {
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(15));
            wait.Until(d => (bool)(d as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0"));
        }
        
        public bool IsElementVisible(By by)
        {
            try
            {
                WaitForElementIsVisible(2, by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }


    }
}

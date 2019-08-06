using System;
using System.Configuration;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace AutomationFramework.Components.Pages
{
    public class JsTestPage : BasePage
    {
        [FindsBy(How = How.Id, Using = "top")]
        protected IWebElement TopInputBox { get; set; }

        [FindsBy(How = How.Id, Using = "left")]
        protected IWebElement LeftInputBox { get; set; }

        [FindsBy(How = How.Id, Using = "process")]
        protected IWebElement ProcessButton { get; set; }

        [FindsBy(How = How.ClassName, Using = "flash")]
        protected IWebElement FindMe { get; set; }

        
        public string GetTopCoordinates()
        {
           var top = ((IJavaScriptExecutor) DriverImplementation.Driver).ExecuteScript("return jQuery('.flash').position().top").ToString();
           var topNumber = Convert.ToInt32(double.Parse(top));

           return topNumber.ToString();
            
        }

        public string GetLeftCoordinates()
        {
            var left = ((IJavaScriptExecutor)DriverImplementation.Driver).ExecuteScript("return jQuery('.flash').position().left").ToString();
            var leftNumber = Convert.ToInt32(double.Parse(left));

            return leftNumber.ToString();

        }

        public void InsertCoordinatesAndClickProcess()
        {
            TopInputBox.SendKeys(GetTopCoordinates());
            LeftInputBox.SendKeys(GetLeftCoordinates());
            ProcessButton.Click();
        }

        public string CheckAlertMessage()
        {
           var message = DriverImplementation.Driver.SwitchTo().Alert().Text;
           AcceptAlert();

           return message;
        }
    }

}

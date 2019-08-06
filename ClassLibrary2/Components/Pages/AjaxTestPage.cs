using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Text.RegularExpressions;

namespace AutomationFramework.Components.Pages
{
    public class AjaxTestPage : BasePage
    {
        [FindsBy(How = How.Id, Using = "x")]
        protected IWebElement XInputBox { get; set; }

        [FindsBy(How = How.Id, Using = "y")]
        protected IWebElement YInputBox { get; set; }

        [FindsBy(How = How.Id, Using = "result")]
        protected IWebElement ResultMessage { get; set; }

        [FindsBy(How = How.Id, Using = "calc")]
        protected IWebElement SumButton { get; set; }

        [FindsBy(How = How.Id, Using = "clear")]
        protected IWebElement ClearButton { get; set; }

        
       public void DoRandomCalculations(string x, string y)
        {
            XInputBox.SendKeys(x);
            YInputBox.SendKeys(y);

            SumButton.Click();

        }

        public string GetCalculationResult()
        {
            WaitForAjax();
            if (ResultMessage.Text.Contains("."))
            {
                var result = ResultMessage.Text.Remove(ResultMessage.Text.Length - 2);
                var result1 = Regex.Replace(result, "[^.0-9]", "");
                return result1;
            }
            return ResultMessage.Text;

        }


    }
}

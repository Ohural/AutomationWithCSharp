using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationFramework.Framework.Enums;
using AutomationFramework.Framework.Helpers;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace AutomationFramework.Components.Pages
{
    public class AppEditPage : BasePage
    {
        [FindsBy(How = How.Name, Using = "description")]
        protected IWebElement DescriptionBox { get; set; }

        [FindsBy(How = How.XPath, Using = "//select/option[@selected = 'selected']")]
        protected IWebElement SelectedCategory { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@value = 'Update']")]
        protected IWebElement UpdateButton { get; set; }




        public void ChangeDescription()
        {
            DescriptionBox.Clear();
            DescriptionBox.SendKeys($"EditedDescription_{RandomHelper.CreateRandomAlphaNumeric(10)}");
        }

        public void ChangeCategory()
        {
            var category = RandomHelper.GetUniqueEnumValue<AppCategory>();
            var oldCategory = SelectedCategory.Text;

            while (oldCategory.Equals($"{category}"))
            {
                category = RandomHelper.GetUniqueEnumValue<AppCategory>();
            }
            var newCategory = DriverImplementation.Driver.FindElement(
                By.XPath($"//select[@name = 'category']/option[contains(text(),'{category.ToString()}')]"));
            newCategory.Click();
        }

        public void EditApplicationWithRandomData()
        {
            ChangeDescription();
            ChangeCategory();
            UpdateButton.Click();
        }
    }
}

using System;
using System.Net.Mime;
using AutomationFramework.Framework.Enums;
using AutomationFramework.Framework.Models;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace AutomationFramework.Components.Pages
{
    public class NewApplicationPage : BasePage
    {
        [FindsBy(How = How.XPath, Using = "//input[@value = 'Create']")]
        private IWebElement CreateButton;

        [FindsBy(How = How.XPath, Using = "//h1[text() = 'New application']")]
        private IWebElement NewApplicationSign;

        [FindsBy(How = How.Name, Using = "title")]
        private IWebElement TitleInputBox;

        [FindsBy(How = How.Name, Using = "description")]
        private IWebElement DescriptionInputBox;

        [FindsBy(How = How.Name, Using = "image")]
        private IWebElement ImageChooseFileButton;

        [FindsBy(How = How.Name, Using = "icon")]
        private IWebElement IconChooseFileButton;

        public void SelectRole(Enum appCategory) => DriverImplementation.Driver.FindElement(By.XPath($"//select[@name = 'category']/option[text() = '{appCategory}']")).Click();

        public bool IsCreateButtonVisible => CreateButton.Displayed;

        public string NewAppText => NewApplicationSign.Text;

        public void AddImage()
        {
            ImageChooseFileButton.SendKeys(@"C:\Automation projects\SeleniumCourseReboot\img2.jpg");
            
        }

        public void AddIcon()
        {
            IconChooseFileButton.SendKeys(@"C:\Automation projects\SeleniumCourseReboot\icon1.jpg");
        }

        public void RegisterNewBlankApp(Application newApplication)
        {
            TitleInputBox.SendKeys(newApplication.Title);
            DescriptionInputBox.SendKeys(newApplication.Description);
            SelectRole(newApplication.Category);
            CreateButton.Click();

        }

        public void RegisterNewAppWithImageAndIcon(Application newApplication)
        {
            TitleInputBox.SendKeys(newApplication.Title);
            DescriptionInputBox.SendKeys(newApplication.Description);
            SelectRole(newApplication.Category);
            AddImage();
            AddIcon();
            CreateButton.Click();

        }

    }
}

using System.Text.RegularExpressions;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace AutomationFramework.Components.Pages
{
    public class AppDetailsPage : BasePage
    {
        public DownloadJsonPage DownloadJsonPage => new DownloadJsonPage();

        [FindsBy(How = How.XPath, Using = "//div[@class = 'name']")]
        protected IWebElement AppNameTitle { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class = 'description' and (contains(text(),'Description:'))]")]
        protected IWebElement AppDescription { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class = 'description' and (contains(text(),'Category:'))]")]
        protected IWebElement AppCategory { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class = 'description' and (contains(text(),'Author:'))]")]
        protected IWebElement AppAuthor { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class = 'downloads']")]
        protected IWebElement NumberOfDownloads { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[contains(@href,'/download')]")]
        protected IWebElement DownloadButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[contains(@href,'/delete')]")]
        protected IWebElement DeleteButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[contains(@href,'/edit')]")]
        protected IWebElement EditButton { get; set; }

        public string GetAppName => AppNameTitle.Text;
        public string GetAppDescription => AppDescription.Text.Replace("Description: ","");
        public string GetAppCategory => AppCategory.Text.Replace("Category: ","");
        public string GetNumberOfDownloads => Regex.Replace(NumberOfDownloads.Text, "[^.0-9]", "");
        public void ClickDownload() => DownloadButton.Click();
        public void ClickEdit() => EditButton.Click();

        public void DeleteApp()
        {
            DeleteButton.Click();
            AcceptAlert();
        }
        
        public void DownloadAppMultipleTimes(int times)
        {
            ClickDownload();
            for (int i=0;i<=times;i++)
            {
                DriverImplementation.Driver.Navigate().Refresh();

            } 
        }

    }
}

using Newtonsoft.Json.Linq;
using NUnit.Framework.Constraints;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using SeleniumExtras.PageObjects;



namespace AutomationFramework.Components.Pages
{
    public class DownloadJsonPage : BasePage
    {
        [FindsBy(How = How.XPath, Using = "//pre")]
        protected IWebElement DownloadJson { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string NumberOfDownloads { get; set; }
        public string Category { get; set; }
        public string IconData { get; set; }
        public string ImageData { get; set; }
        public string AuthorLastName { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLogin { get; set; }
        public string AuthorRole { get; set; }
        
        public string JsonString()
        {
           
            if (IsElementVisible(By.Id("rawdata-tab")))
            {
                 var rawData = DriverImplementation.Driver.FindElement(By.Id("rawdata-tab"));
                 rawData.Click();
                return DownloadJson.Text;
            }

            return DownloadJson.Text;

        }
        
        public DownloadJsonPage JsonAttributes()
        {
            JObject obj = JObject.Parse(JsonString());
                
            var jsonAttributes = new DownloadJsonPage();

            jsonAttributes.Title = (string)obj.SelectToken("title");
            jsonAttributes.Description = (string)obj.SelectToken("description");
            jsonAttributes.NumberOfDownloads = (string) obj.SelectToken("numberOfDownloads");
            jsonAttributes.IconData = (string) obj.SelectToken("iconData");
            jsonAttributes.ImageData = (string)obj.SelectToken("imageData");
            jsonAttributes.Category = (string) obj.SelectToken("category").SelectToken("title");
            jsonAttributes.AuthorLastName = (string)obj.SelectToken("author").SelectToken("lname");
            jsonAttributes.AuthorFirstName = (string)obj.SelectToken("author").SelectToken("fname");
            jsonAttributes.AuthorLogin = (string)obj.SelectToken("author").SelectToken("name");
            jsonAttributes.AuthorRole = (string)obj.SelectToken("author").SelectToken("roleModel").SelectToken("title");

            return jsonAttributes;
        }

        
    }
}

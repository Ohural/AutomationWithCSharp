using System.IO;
using System.Reflection;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Configuration;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.IE;

namespace AutomationFramework
{
    public class DriverImplementation
    {
        public static ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
        public static IWebDriver Driver => driver.Value;
        public string BrowserName;
        public static string DriversDirectory = Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).Parent?.Parent?.FullName + "\\Drivers";

        public DriverImplementation(string browser)
        {
            this.BrowserName = browser;

        }


        [SetUp]
        public void SetUp()
        {
            InitBrowser();
            OpenHomePage();
        }

        public void InitBrowser()
        {
          
            if (BrowserName.Equals("Chrome"))
            {
                driver.Value = new ChromeDriver(DriversDirectory);
            }
            else if (BrowserName.Equals("Firefox"))
            {
                driver.Value = new FirefoxDriver(DriversDirectory);
            }
            else if (BrowserName.Equals("IE"))
            {
                driver.Value = new InternetExplorerDriver(DriversDirectory);
            }
            Driver.Manage().Window.Maximize();


        }


        public static void OpenHomePage()
        {
            Driver.Navigate().GoToUrl(ConfigurationManager.AppSettings.Get("basicUrl"));
        }

        [TearDown]
        public void CloseBrowser()
        {
            ExtentReport.GenerateReport();
            Driver.Close();
        }
    }
}

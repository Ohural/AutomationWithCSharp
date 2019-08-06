using System;
using System.IO;
using System.Reflection;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace AutomationFramework
{
    class ExtentReport
    {
        private static readonly Lazy<ExtentReports> _lazy = new Lazy<ExtentReports>(() => new ExtentReports());

        public static ExtentReports Instance { get { return _lazy.Value; } }

        private static string screenshotPathHtml =
            Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).Parent?.Parent
                ?.FullName + "\\Report\\ExtentScreenshot.html";

        
        public static void GenerateReport()
        {
            // initialize the HtmlReporter
            
            var htmlReporter = new ExtentHtmlReporter(screenshotPathHtml);
            
            // attach only HtmlReporter
            Instance.AttachReporter(htmlReporter);

            var name = TestContext.CurrentContext.Test.MethodName;
            var test = Instance.CreateTest(name);
            
            var stackTrace = "<pre>" + TestContext.CurrentContext.Result.StackTrace + "</pre>";
            var errorMessage = TestContext.CurrentContext.Result.Message;
            CaptureScreenShot($"{name}.png");
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                test.Log(Status.Fail, "Snapshot below: " + test.AddScreenCaptureFromPath($"{name}.png"));
                test.Log(Status.Fail, stackTrace + errorMessage);
            }
            Instance.Flush();
        }

        private static void CaptureScreenShot(string screenShotName)
        {
            var ss = DriverImplementation.Driver.TakeScreenshot();
            var captureLocation = Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).Parent?.Parent
                                      ?.FullName + $"\\Report\\{screenShotName}";
            ss.SaveAsFile(captureLocation, ScreenshotImageFormat.Png);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationFramework;
using AutomationFramework.Components;
using AutomationFramework.Components.Pages;
using AutomationFramework.Framework.Models;
using NUnit.Framework;

namespace Tests.JsTest
{

    [Parallelizable(ParallelScope.All)]
    [TestFixture("Chrome")]
    [TestFixture("Firefox")]
    
    public class JsTest: DriverImplementation
    {
        [ThreadStatic] private static LoginPage _loginPage;
        [ThreadStatic] private static JsTestPage _jsTestPage;

        public JsTest(string browser) : base(browser)
        {
        }
        [SetUp]
        public void SetUp()
        {
            _loginPage = new LoginPage();
            _jsTestPage = new JsTestPage();
        }

        [Test]

        public void JavaScriptTest()
        {
            _loginPage.Login(User.GetDefaultUser().Login, User.GetDefaultUser().Password);
            SiteNavigator.GoToJsTestPage();
            _jsTestPage.InsertCoordinatesAndClickProcess();
            Assert.AreEqual("Whoo Hoooo! Correct!",_jsTestPage.CheckAlertMessage());
        }
    }
}

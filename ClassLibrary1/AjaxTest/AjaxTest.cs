using System;
using AutomationFramework;
using AutomationFramework.Components;
using AutomationFramework.Components.Pages;
using AutomationFramework.Framework.Helpers;
using AutomationFramework.Framework.Models;
using NUnit.Framework;

namespace Tests.AjaxTest
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture("Chrome")]
    [TestFixture("Firefox")]

    public class AjaxTest : DriverImplementation
    {
        [ThreadStatic] private static LoginPage _loginPage;
        [ThreadStatic] private static AjaxTestPage _ajaxTestPage;
        [ThreadStatic] private static User user;

        public AjaxTest(string browser) : base(browser)
        {
        }

        [SetUp]
        public void SetUp()
        {
            user = User.GetDefaultUser();
            _loginPage = new LoginPage();
            _ajaxTestPage = new AjaxTestPage();
        }

        [Test]
        public void CorrectCalculationTest()
        {
            var x = RandomHelper.CreateRandomNumber(999);
            var y = RandomHelper.CreateRandomNumber(999);
            var expectedSum = (x + y).ToString();

            _loginPage.Login(user.Login, user.Password);
            SiteNavigator.GoToAjaxTestPage();
            _ajaxTestPage.DoRandomCalculations(x.ToString(), y.ToString());


            var actualSum = _ajaxTestPage.GetCalculationResult();

            Assert.AreEqual(actualSum, expectedSum, "Calculation is not correct");
        }

        [Test]
        public void IncorrectCalculationTest()
        {
            var x = RandomHelper.CreateRandomAlphabetic(3);
            var y = RandomHelper.CreateRandomNumber(999);

            _loginPage.Login(user.Login, user.Password);
            SiteNavigator.GoToAjaxTestPage();
            _ajaxTestPage.DoRandomCalculations(x, y.ToString());

            Assert.True(_ajaxTestPage.GetCalculationResult().Contains("Incorrect data"));
        }
    }
}
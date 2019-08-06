using System;
using AutomationFramework;
using AutomationFramework.Components;
using AutomationFramework.Components.Pages;
using AutomationFramework.Components.Panels;
using AutomationFramework.Framework.Helpers;
using AutomationFramework.Framework.Models;
using NUnit.Framework;

namespace Tests.LoginTests
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture("Chrome")]
    [TestFixture("Firefox")]
    

    public class LoginTests : DriverImplementation
    {
        [ThreadStatic] private static LoginPage _loginPage;
        [ThreadStatic] private static HeaderPanel _header;
        
        public LoginTests(string browser) : base(browser)
        {
        }

        [SetUp]
        public void SetUp()
        {
            _loginPage = new LoginPage();
            _header = new HeaderPanel();
        }

        
        [Test]
        public void CorrectCredentialsLogin()
        {
            _loginPage.Login(User.GetDefaultUser().Login, User.GetDefaultUser().Password);
            Assert.True(_header.WelcomeText.Contains(User.GetDefaultUser().FirstName));
        }

        [Test]
        public void IncorrectCredentialsLogin()
        {
            var incorrectLogin = RandomHelper.CreateRandomAlphaNumeric(6);
            var incorrectPassword = RandomHelper.CreateRandomAlphaNumeric(6);
            _loginPage.Login(incorrectLogin,incorrectPassword);
            Assert.True(_loginPage.GetFlashMessage().Contains("invalid username or password"));
        }

        [Test]

        public void BasicAuthLogin()
        {
            
            _loginPage.LoginWithBasicAuth(User.GetDefaultUser().Login, User.GetDefaultUser().Password);

            Assert.True(_header.WelcomeText.Contains(User.GetDefaultUser().FirstName));
        }
    }
}

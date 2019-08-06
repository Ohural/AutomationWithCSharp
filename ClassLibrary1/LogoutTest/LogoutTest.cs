using System;
using System.Configuration;
using AutomationFramework;
using AutomationFramework.Components.Pages;
using AutomationFramework.Framework.Helpers;
using AutomationFramework.Framework.Models;
using NUnit.Framework;

namespace Tests.LogoutTest
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture("Chrome")]
    [TestFixture("Firefox")]

    public class LogoutTest : DriverImplementation

    {
    [ThreadStatic] private static LoginPage _loginPage;
    [ThreadStatic] private static HomePage _homePage;
    

    public LogoutTest(string browser) : base(browser)
    {
    }
    [SetUp]
    public void SetUp()
    {
        _loginPage = new LoginPage();
        _homePage = new HomePage();
        
    }

    [Test]

    public void LoginOpenTabAndLogoutTest()
    {
        _loginPage.Login(User.GetDefaultUser().Login, User.GetDefaultUser().Password);
        TabActions.OpenUrlAndSwitchToNewTab(ConfigurationManager.AppSettings.Get("myAppsURL"));

        Assert.True(_homePage.HeaderPanel.WelcomeText.Contains(User.GetDefaultUser().FirstName));

        _homePage.HeaderPanel.Logout();
        TabActions.CloseNewTabAndSwitchToPrevious();
        _homePage.HeaderPanel.ClickOnMyAppsButton();

        Assert.True(_loginPage.LoginSignText.Contains("Login"));
        Assert.True(_loginPage.IsLoginButtonVisible(),"Login button is not visible");

    }

    }

}

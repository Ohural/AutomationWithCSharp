using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationFramework;
using AutomationFramework.Components;
using AutomationFramework.Components.Pages;
using AutomationFramework.Framework.Enums;
using AutomationFramework.Framework.Models;
using NUnit.Framework;

namespace Tests.ApplicationTest
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture("Chrome")]
    [TestFixture("Firefox")]

    public class ApplicationTest : DriverImplementation
    {

        [ThreadStatic] private static LoginPage _loginPage;
        [ThreadStatic] private static Application newApp;
        [ThreadStatic] private static RegisterPage _registerPage;
        [ThreadStatic] private static MyApplicationsPage _myApplicationsPage;
        [ThreadStatic] private static NewApplicationPage _newApplicationPage;
        [ThreadStatic] private static User newUser;
        [ThreadStatic] private static AppDetailsPage _appDetailsPage;
        [ThreadStatic] private static DownloadJsonPage _json;
        [ThreadStatic] private static AppEditPage _appEditPage;
        [ThreadStatic] private static HomePage _homePage;
        

        public ApplicationTest(string browser) : base(browser)
        {
        }

        [SetUp]
        public void SetUp()
        {
            newApp = Application.GenerateNewApplication();
            newUser = User.GenerateNewRandomUser();
            _loginPage = new LoginPage();
            _registerPage = new RegisterPage();
            _myApplicationsPage = new MyApplicationsPage();
            _newApplicationPage = new NewApplicationPage();
            _appDetailsPage = new AppDetailsPage();
            _json = new DownloadJsonPage();
            _appEditPage = new AppEditPage();
            _homePage = new HomePage();
            
        }

        [Test]
        public void CheckDownloadedAppInfo()
        {
            _loginPage.Login(User.GetDefaultUser().Login, User.GetDefaultUser().Password);
            _homePage.SelectRandomApp();

            var appName = _appDetailsPage.GetAppName;
            var appDescription = _appDetailsPage.GetAppDescription;
            var appCategory = _appDetailsPage.GetAppCategory;
            var expectedDownloads = (Convert.ToInt32(_appDetailsPage.GetNumberOfDownloads) + 1).ToString();
            

            _appDetailsPage.ClickDownload();

            var json = _json.JsonAttributes();

            Assert.AreEqual(expectedDownloads, json.NumberOfDownloads, "Application was not downloaded");
            Assert.AreEqual(appName, json.Title, "Application downloaded with wrong name");
            Assert.AreEqual(appDescription, json.Description, "Application downloaded with wrong description");
            Assert.AreEqual(appCategory,json.Category,"Application downloaded with wrong category");
        }

        [Test]
        public void CreateNewAppWithoutImage()
        {
            SiteNavigator.GoToRegistrationForm();
            _registerPage.RegisterNewRandomUser(newUser, Role.DEVELOPER);
            SiteNavigator.GoToNewAppPage();
            _newApplicationPage.RegisterNewBlankApp(newApp);

            Assert.True(_myApplicationsPage.CheckNewAppPresence(newApp.Title));

            _myApplicationsPage.GoToAppDetails(newApp.Title);

            Assert.True(_appDetailsPage.GetAppName.Contains(newApp.Title));
            Assert.True(_appDetailsPage.GetAppDescription.Contains(newApp.Description));
            Assert.True(_appDetailsPage.GetAppCategory.Contains(newApp.Category.ToString()));

            var expectedDownloads = (Convert.ToInt32(_appDetailsPage.GetNumberOfDownloads) + 1).ToString();

            _appDetailsPage.ClickDownload();

            var json = _json.JsonAttributes();

            Assert.AreEqual(expectedDownloads, json.NumberOfDownloads, "Application was not downloaded");
            Assert.AreEqual(newApp.Title,json.Title,"Application downloaded with wrong name");
            Assert.AreEqual(newApp.Description,json.Description,"Application downloaded with wrong description");
            Assert.AreEqual(newUser.Name,json.AuthorLogin,"Application downloaded with wrong user login");
            Assert.AreEqual(newUser.FirstName, json.AuthorFirstName,"Application downloaded with wrong user first name");
            Assert.AreEqual(newUser.LastName, json.AuthorLastName, "Application downloaded with wrong user last name");
            Assert.AreEqual($"{Role.DEVELOPER}", json.AuthorRole, "Application downloaded with wrong user role");
            
        }

        [Test]
        public void EditExistingApp()
        {
            SiteNavigator.GoToRegistrationForm();
            _registerPage.RegisterNewRandomUser(newUser, Role.DEVELOPER);
            SiteNavigator.GoToNewAppPage();
            _newApplicationPage.RegisterNewBlankApp(newApp);
            _myApplicationsPage.GoToAppDetails(newApp.Title);

            var oldDescription = _appDetailsPage.GetAppDescription;
            var oldCategory = _appDetailsPage.GetAppCategory;

            _appDetailsPage.ClickEdit();
            _appEditPage.EditApplicationWithRandomData();

            Assert.True(_appDetailsPage.GetFlashMessage().Contains("Application edited"));

            SiteNavigator.GoToMyAppsPage();
            _myApplicationsPage.GoToAppDetails(newApp.Title);

            var editedDescription = _appDetailsPage.GetAppDescription;
            var editedCategory = _appDetailsPage.GetAppCategory;

            Assert.AreNotEqual(oldDescription,editedDescription);
            Assert.AreNotEqual(oldCategory, editedCategory);


        }

        [Test]
        public void DeleteApplication()
        {
            SiteNavigator.GoToRegistrationForm();
            _registerPage.RegisterNewRandomUser(newUser, Role.DEVELOPER);
            SiteNavigator.GoToNewAppPage();
            _newApplicationPage.RegisterNewBlankApp(newApp);
            _myApplicationsPage.GoToAppDetails(newApp.Title);
            _appDetailsPage.DeleteApp();

            Assert.True(_appDetailsPage.GetFlashMessage().Contains("Deleted"));

            SiteNavigator.GoToMyAppsPage();

            Assert.False(_myApplicationsPage.CheckNewAppPresence(newApp.Title));
        }

        [Test]

        public void CreateAppWithImage()
        {
            SiteNavigator.GoToRegistrationForm();
            _registerPage.RegisterNewRandomUser(newUser, Role.DEVELOPER);
            SiteNavigator.GoToNewAppPage();
            _newApplicationPage.RegisterNewAppWithImageAndIcon(newApp);
            _myApplicationsPage.GoToAppDetails(newApp.Title);

            _appDetailsPage.ClickDownload();

            var json = _json.JsonAttributes();

            Assert.False(_appDetailsPage.IsEmpty(json.ImageData));
            Assert.False(_appDetailsPage.IsEmpty(json.IconData));

        }

        [Test]

        public void CheckMostPopularApps()
        {
            SiteNavigator.GoToRegistrationForm();
            _registerPage.RegisterNewRandomUser(newUser, Role.DEVELOPER);
            SiteNavigator.GoToNewAppPage();
            _newApplicationPage.RegisterNewBlankApp(newApp);
            _myApplicationsPage.GoToAppDetails(newApp.Title);
            _appDetailsPage.DownloadAppMultipleTimes(10);
            SiteNavigator.GoToHomePage();

            Assert.True(_homePage.CheckAppIsPopular(newApp.Title));

            _homePage.ClickOnPopularApp(newApp.Title);

            Assert.AreEqual(newApp.Title,_appDetailsPage.GetAppName);



        }
    }
}

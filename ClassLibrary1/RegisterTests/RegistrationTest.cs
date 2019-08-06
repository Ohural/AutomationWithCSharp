using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using AutomationFramework;
using AutomationFramework.Components;
using AutomationFramework.Components.Pages;
using AutomationFramework.Components.Panels;
using AutomationFramework.Framework.Enums;
using AutomationFramework.Framework.Helpers;
using AutomationFramework.Framework.Models;
using CsvHelper;
using NUnit.Framework;


namespace Tests.RegisterTests
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture("Chrome")]
    [TestFixture("Firefox")]
    

    public class RegistrationTest : DriverImplementation
    {
        [ThreadStatic] private static LoginPage _loginPage;
        [ThreadStatic] private static User newUser;
        [ThreadStatic] private static RegisterPage _registerPage;
        [ThreadStatic] private static HeaderPanel _header;
        [ThreadStatic] private static MyApplicationsPage _myApplicationsPage;
        [ThreadStatic] private static NewApplicationPage _newApplicationPage;

        public RegistrationTest(string browser) : base(browser)
        {
        }
    
       private static IEnumerable UserData() => CsvUserModel.GetTestData();

        [SetUp]
        public void SetUp()
        {
            newUser = User.GenerateNewRandomUser();
            _registerPage = new RegisterPage();
            _loginPage = new LoginPage();
            _header = new HeaderPanel();
            _myApplicationsPage = new MyApplicationsPage();
            _newApplicationPage = new NewApplicationPage();
        }

        [Test]

        public void RegisterNewUserTest()
        {
            SiteNavigator.GoToRegistrationForm();
            _registerPage.RegisterNewRandomUser(newUser, Role.USER);
            Assert.AreEqual(_header.WelcomeText, $"Welcome {newUser.FirstName} {newUser.LastName}");
        }

        [Test]
        public void RegisterUserAndRelogin()
        {
            SiteNavigator.GoToRegistrationForm();
            _registerPage.RegisterNewRandomUser(newUser, Role.USER);
            Assert.AreEqual(_header.WelcomeText, $"Welcome {newUser.FirstName} {newUser.LastName}");
            _header.Logout();

            _loginPage.Login(newUser.Name, newUser.Password);
            Assert.AreEqual(_header.WelcomeText, $"Welcome {newUser.FirstName} {newUser.LastName}");

        }

        [Test]

        public void RegisterAsDevAndCheckUploadOpensTest()
        {
            SiteNavigator.GoToRegistrationForm();
            _registerPage.RegisterNewRandomUser(newUser, Role.DEVELOPER);
            _header.ClickOnMyAppsButton();
            _myApplicationsPage.ClickAddNewApp();

            Assert.True(_newApplicationPage.IsCreateButtonVisible,"Create button is not visible");
            Assert.True(_newApplicationPage.NewAppText.Contains("New application"));
        }

        [Test]

        public void RegisterAsUserAndCheckNoUploadTest()
        {
            SiteNavigator.GoToRegistrationForm();
            _registerPage.RegisterNewRandomUser(newUser, Role.USER);
            
            Assert.False(_header.CheckIsMyAppsVisible(),"My applications tab is visible for regular user");
            
        }

       
        [Test,TestCaseSource("UserData")]

        public void RegisterCsvUsers(string name, string fname, string lname, string password, string role)
        {
            SiteNavigator.GoToRegistrationForm();
            _registerPage.RegisterCsvUser(name, fname, lname, password, role);

            Assert.AreEqual(_header.WelcomeText, $"Welcome {fname} {lname}");

        }





    }
}

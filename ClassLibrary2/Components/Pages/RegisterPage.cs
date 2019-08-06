using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using AutomationFramework.Framework.Models;


namespace AutomationFramework.Components.Pages
{
    public class RegisterPage : BasePage
    {
        [FindsBy(How = How.XPath, Using = "//input[@name = 'name']")]
        private IWebElement Name;

        [FindsBy(How = How.XPath, Using = "//input[@name = 'fname']")]
        private IWebElement FirstName;

        [FindsBy(How = How.XPath, Using = "//input[@name = 'lname']")]
        private IWebElement LastName;

        [FindsBy(How = How.XPath, Using = "//input[@name = 'password']")]
        private IWebElement Password;

        [FindsBy(How = How.XPath, Using = "//input[@name = 'passwordConfirm']")]
        private IWebElement ConfirmPassword;

        [FindsBy(How = How.XPath, Using = "//select[@name = 'role']")]
        private IWebElement RoleDropdownBox;

        [FindsBy(How = How.XPath, Using = "//select[@name = 'role']/option")]
        private IList<IWebElement> RoleDropdown;

        [FindsBy(How = How.XPath, Using = "//input[@type = 'submit']")]
        private IWebElement SubmitButton;

        public void SelectRole(Enum Role) => DriverImplementation.Driver.FindElement(By.XPath($"//select[@name = 'role']/option[@value = '{Role}']")).Click();
        public void SelectRole(string Role) => DriverImplementation.Driver.FindElement(By.XPath($"//select[@name = 'role']/option[@value = '{Role}']")).Click();

        public void RegisterNewRandomUser(User newUser, Enum role)
        {
            Name.SendKeys(newUser.Name);
            FirstName.SendKeys(newUser.FirstName);
            LastName.SendKeys(newUser.LastName);
            Password.SendKeys(newUser.Password);
            ConfirmPassword.SendKeys(newUser.ConfirmPassword);
            SelectRole(role);
            SubmitButton.Click();
        }

        public void RegisterCsvUser(string name, string fname,string lname, string password, string role)
        {
            Name.SendKeys(name);
            FirstName.SendKeys(fname);
            LastName.SendKeys(lname);
            Password.SendKeys(password);
            ConfirmPassword.SendKeys(password);
            SelectRole(role);
            SubmitButton.Click();
            
        }
        
    }
}

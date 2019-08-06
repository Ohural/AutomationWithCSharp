using System.Configuration;
using AutomationFramework.Framework.Enums;
using AutomationFramework.Framework.Helpers;

namespace AutomationFramework.Framework.Models
{
    public class User
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Name { get; set; }

        public string ConfirmPassword { get; set; }

        public Role RoleName { get; set; }



        public static User GetDefaultUser()
        {
            var defaultUser = new User();

            defaultUser.Login = ConfigurationManager.AppSettings.Get("adminLogin");
            defaultUser.Password = ConfigurationManager.AppSettings.Get("adminPassword");
            defaultUser.FirstName = ConfigurationManager.AppSettings.Get("adminFirstName");
            defaultUser.LastName = ConfigurationManager.AppSettings.Get("adminLastName");
            return defaultUser;
        }

        public static User GenerateNewRandomUser()
        {
            var newUser = new User();

            newUser.Name = RandomHelper.CreateRandomString("Login");
            newUser.FirstName = RandomHelper.CreateRandomString("FirstName");
            newUser.LastName = RandomHelper.CreateRandomString("LastName");
            newUser.Password = RandomHelper.CreateRandomAlphaNumeric(6);
            newUser.ConfirmPassword = newUser.Password;
            newUser.RoleName = RandomHelper.GetUniqueEnumValue<Role>();

            return newUser;
        }


    }
}

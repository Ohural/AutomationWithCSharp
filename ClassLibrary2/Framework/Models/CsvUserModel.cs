using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using NUnit.Framework;

namespace AutomationFramework.Framework.Models
{
    public static class CsvUserModel
    {
        private static string filePath =
            @"C:\Automation projects\SeleniumCourseReboot\ClassLibrary2\Framework\Models\UserData.csv";

        public static IEnumerable<string[]> GetTestData()
        {
            
            using (var csv = new CsvReader(new StreamReader(filePath), true))
            {
                while (csv.Read())
                {
                    var name = csv[0];
                    var fname = csv[1];
                    var lname = csv[2];
                    var password = csv[3];
                    var role = csv[4];

                    yield return new[] { name, fname, lname, password, role };
                }
            }
        }
    }
}

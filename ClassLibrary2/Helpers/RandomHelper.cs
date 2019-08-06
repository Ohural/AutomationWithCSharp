using System;
using System.Linq;

namespace AutomationFramework.Framework.Helpers
{
    public class RandomHelper
    {
        private static Random random = new Random();
        private const string Numbers = "0123456789";
        private const string Alphabetic = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private const string AlphaNumeric = Numbers + Alphabetic;

        public static string CreateRandomString(string text)
        {
            return $"{text}{random.Next(999)}";
        }
        public static string CreateRandomAlphaNumeric(int length)
        {
            return new string(Enumerable.Repeat(AlphaNumeric, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

        }

        public static int CreateRandomNumber(int maxValue)
        {
            return random.Next(maxValue);
        }

        public static string CreateRandomAlphabetic(int length)
        {
            return new string(Enumerable.Repeat(Alphabetic, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static T GetUniqueEnumValue<T>()
        {
            var value = Enum.GetValues(typeof(T));

            return (T)value.GetValue(random.Next(value.Length));
        }
    }
}


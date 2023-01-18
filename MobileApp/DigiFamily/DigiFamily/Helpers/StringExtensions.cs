using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace DigiFamily.Helpers
{
    public static class StringExtensions
    {
        public static bool IsValidEmail(this string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    IdnMapping idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        public static string ToPhone(this string inputStr)
        {
            if (string.IsNullOrEmpty(inputStr))
                return inputStr;
            return Regex.Replace(inputStr, @"(\d{5})(\d{5})", "$1 $2");
        }
        public static string RemovePhone(this string inputStr)
        {
            if (string.IsNullOrEmpty(inputStr))
                return inputStr;
            //return Regex.Replace(inputStr, "[(|)|-]", "");
            return Regex.Replace(inputStr, @"\D", string.Empty);
        }
        public static int? ToNullableInt(this string s)
        {
            if (int.TryParse(s, out int i)) return i;
            return null;
        }

        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        
        public static bool IsValidPasswordStrength(this string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            try
            {
                return Regex.IsMatch(password,
                    @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{6,})",
                   RegexOptions.None, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        public static bool IsValidPhoneNumber(this string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            try
            {
                return Regex.IsMatch(password,
                    @"^[6-9]\d{4}[\s ]\d{5}$",
                   RegexOptions.None, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}

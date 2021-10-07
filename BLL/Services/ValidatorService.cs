using BLL.Interfaces;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace BLL.Services
{
    public class ValidatorService : IValidatorService
    {
        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

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
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            try
            {
                //min 1 lowercase, 1 uppercase, 1 number, 1 special character (non-letter and non-number)
                //8+ characters
                return Regex.IsMatch(password,
                    @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!*()@%&])[\s\S]{8,}$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public bool IsValidPhoneNumber(string phoneNumber)
        {
            if(string.IsNullOrEmpty(phoneNumber))
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(phoneNumber,
                    @"^\+?(\d[\d\-. ]+)?(\([\d\-. ]+\))?[\d\-. ]+\d$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}

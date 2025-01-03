using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AptEMS.Models
{
    public class MobileNumberValidation : ValidationAttribute
    {
        private readonly string _countryCode;

        public MobileNumberValidation(string countryCode)
        {
            _countryCode = countryCode;
        }

        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return false;

            string mobileNumber = value.ToString();

            // Check if the number starts with the country code
            if (!mobileNumber.StartsWith(_countryCode))
                return false;

            // Remove the country code and check if the rest is numeric and has a valid length
            string numberWithoutCountryCode = mobileNumber.Substring(_countryCode.Length);
            return numberWithoutCountryCode.All(char.IsDigit) && numberWithoutCountryCode.Length == 10;
        }
    }
}

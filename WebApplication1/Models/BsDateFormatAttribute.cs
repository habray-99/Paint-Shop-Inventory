using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebApplication1.Models
{
    public class BsDateFormatAttribute : ValidationAttribute
    {
        private static readonly Regex BsDateRegex = new Regex(@"^20\d{2}-((0[1-9])|(1[0-2]))-((0[1-9])|([12][0-9])|(3[01]))$", RegexOptions.Compiled);

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var str = value as string;
            if (str != null && BsDateRegex.IsMatch(str))
                return ValidationResult.Success;

            return new ValidationResult($"{validationContext.DisplayName} must be in BS format YYYY-MM-DD");
        }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace WebApplication1.Models
{
    public class BsDateFormatAttribute : ValidationAttribute
    {
        private static readonly Regex BsDateRegex = new Regex(@"^20\d{2}-((0[1-9])|(1[0-2]))-((0[1-9])|([12][0-9])|(3[01]))$", RegexOptions.Compiled);

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                Debug.Assert(ValidationResult.Success != null);
                return ValidationResult.Success;
            }

            if (value is string str && BsDateRegex.IsMatch(str))
                if (ValidationResult.Success != null)
                    return ValidationResult.Success;

            return new ValidationResult($"{validationContext.DisplayName} must be in BS format YYYY-MM-DD");
        }
    }
}

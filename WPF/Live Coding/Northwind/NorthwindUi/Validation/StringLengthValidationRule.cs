using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NorthwindUi.Validation
{
    public class StringLengthValidationRule : ValidationRule
    {
        public int Length { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string text = value?.ToString();

            if (string.IsNullOrWhiteSpace(text) || text.Length!=this.Length)
            {
                return new ValidationResult(false, $"Text hat nicht {this.Length} Zeichen!");
            }

            return new ValidationResult(true, "");
        }
    }
}

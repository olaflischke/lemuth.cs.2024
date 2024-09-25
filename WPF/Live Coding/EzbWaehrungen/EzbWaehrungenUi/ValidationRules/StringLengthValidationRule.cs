using System.Globalization;
using System.Windows.Controls;

namespace EzbWaehrungenUi.ValidationRules;

public class StringLengthValidationRule : ValidationRule
{
    public int MinLength { get; set; } = 0;
    public int MaxLength { get; set; } = 40;

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        if (value is String text)
        {
            if (!string.IsNullOrWhiteSpace(text) && text.Length >= MinLength && text.Length <= MaxLength)
            {
                return new ValidationResult(true, "");
            }
            else
            {
                return new ValidationResult(false, "String hat nicht die passende Länge");
            }
        }

        return new ValidationResult(false, "Kein String");
    }
}

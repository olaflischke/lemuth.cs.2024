using System.Globalization;
using System.Windows.Controls;

namespace EzbWaehrungenUi.ValidationRules;

public class DoubleRangeValidationRule : ValidationRule
{
    public double MinValue { get; set; } = 0;
    public double MaxValue { get; set; } = 1000;

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        double betrag;
        if (Double.TryParse(value?.ToString(), out betrag))
        {
            if (betrag > MaxValue || betrag < MinValue)
            {
                return new ValidationResult(false, "Betrag außerhalb des gültigen Bereichs");
            }
            return new ValidationResult(true, "");
        }

        return new ValidationResult(false, "Eingabe ist keine Zahl");
    }
}

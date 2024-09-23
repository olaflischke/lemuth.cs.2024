
using System.Globalization;
using System.Windows.Data;

namespace EzbWaehrungenUi.Converter;

public class CurrencyConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            double amount = System.Convert.ToDouble(values[0]);
            double rate = System.Convert.ToDouble(values[1]);

            return (amount / rate);//.ToString();

        }
        catch (Exception)
        {
            //throw;
            return null;
        }

    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}



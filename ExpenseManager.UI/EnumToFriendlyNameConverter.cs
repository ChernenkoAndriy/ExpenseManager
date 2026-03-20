using System;
using System.Globalization;
using System.Windows.Data;

namespace ExpenseManager.UI
{
    public class EnumToFriendlyNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;

            string enumString = value.ToString();

            return enumString switch
            {
                "UAH" => "₴ (Гривня)",
                "USD" => "$ (Долар США)",
                "EUR" => "€ (Євро)",
                "PLN" => "zł (Злотий)",
                "Food" => "Харчування",
                "Transport" => "Транспорт",
                "Entertainment" => "Розваги",
                "Health" => "Здоров'я",
                "Salary" => "Зарплата",
                "Other" => "Інше",
                _ => enumString
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
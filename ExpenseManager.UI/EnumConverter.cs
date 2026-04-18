using System;
using System.Globalization;
using System.Windows.Data;

namespace ExpenseManager.UI
{
    public class EnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;

            if (parameter != null && (parameter.ToString() == "All" || parameter.ToString() == "Income" || parameter.ToString() == "Expense"))
            {
                return value.ToString() == parameter.ToString();
            }

            if (parameter?.ToString() == "IsNegative")
            {
                if (decimal.TryParse(value.ToString(), out decimal balance))
                    return balance < 0;
                return false;
            }

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
                "All" => "Усі",
                "Income" => "Доходи",
                "Expense" => "Витрати",
                _ => enumString
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isChecked && isChecked && parameter != null)
            {
                return parameter.ToString();
            }

            return Binding.DoNothing;
        }
    }
}
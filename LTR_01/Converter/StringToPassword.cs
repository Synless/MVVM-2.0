using System;
using System.Globalization;
using System.Windows.Data;

namespace LTR_01.Converter
{
    class StringToPassword : IValueConverter
    {
        private string password;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            password = "";
            string tmp = value.ToString();
            foreach (char c in tmp)
            {
                password += '●';
            }
            return password;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
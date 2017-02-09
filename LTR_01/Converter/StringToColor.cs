using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace LTR_01.Converter
{
    class StringToColor : IValueConverter
    {
        private SolidColorBrush red;
        private SolidColorBrush yellow;
        private SolidColorBrush green;
        private SolidColorBrush blue;

        public StringToColor()
        {
            System.Drawing.Color r = Properties.Settings.Default.red;
            System.Drawing.Color y = Properties.Settings.Default.yellow;
            System.Drawing.Color g = Properties.Settings.Default.green;
            System.Drawing.Color b = Properties.Settings.Default.blue;
            red     = new SolidColorBrush(Color.FromRgb(r.R, r.G, r.B));
            yellow  = new SolidColorBrush(Color.FromRgb(y.R, y.G, y.B));
            green   = new SolidColorBrush(Color.FromRgb(g.R, g.G, g.B));
            blue    = new SolidColorBrush(Color.FromRgb(b.R, b.G, b.B));
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string tmp = value.ToString();
            if (tmp == "red")
            {
                return red;
            }
            else if (tmp=="yellow")
            {
                return yellow;
            }
            else if(tmp=="green")
            {
                return green;
            }
            else
            {
                return blue;
            }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

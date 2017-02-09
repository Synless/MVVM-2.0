using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace LTR_01.Converter
{
    class ApertureToColor : IValueConverter
    {
        private SolidColorBrush red;
        private SolidColorBrush yellow;
        private SolidColorBrush green;
        private SolidColorBrush blue;
        private const double scale = 70;

        public ApertureToColor()
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
            double tmp = Math.Min(int.Parse(value.ToString()) / scale,1);
            
            int dred =      Math.Min(255, (int)(red.Color.R * (1 - tmp) + green.Color.R * tmp));
            int dgreen =    Math.Min(255, (int)(red.Color.G * (1 - tmp) + green.Color.G * tmp));
            int dblue =     Math.Min(255, (int)(red.Color.B * (1 - tmp) + green.Color.B * tmp));

            return new SolidColorBrush(Color.FromRgb((byte)dred, (byte)dgreen, (byte)dblue));            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace EasyGraphics.Convertors
{
    public class PictureNameToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var pictureName = (string) value;

            if (pictureName == null)
            {
                pictureName = "NoImage";
            }

            var uri = new Uri($@"../images/{pictureName}.jpg", UriKind.Relative);
            return new BitmapImage(uri);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

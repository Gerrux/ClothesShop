using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.IO;

namespace GRRX.ComponentModels
{
    public class BinaryImageConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
            {
                return null;
            }

            if (value is not byte[] bytes)
            {
                return null;
            }
            
            BitmapImage image = new BitmapImage();
            using (MemoryStream imageStream = new MemoryStream())
            {
                imageStream.Write(bytes, 0, bytes.Length);
                imageStream.Seek(0, SeekOrigin.Begin);

                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnDemand;
                image.StreamSource = imageStream;
                image.EndInit();
                image.Freeze();
            }
            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

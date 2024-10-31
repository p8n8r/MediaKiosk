using MediaKiosk.DisplayDialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace MediaKiosk
{
    public static class Utility
    {
        private static Random random = new Random();
        public static IDisplayDialog displayDialog;

        public static byte[] ConvertBitmapImageToBytes(BitmapImage image)
        {
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (MemoryStream stream = new MemoryStream())
            {
                try
                {
                    encoder.Save(stream);
                    return stream.ToArray();
                }
                catch (InvalidOperationException e) { displayDialog.ShowErrorMessageBox(e.Message); }
                catch (NotSupportedException e) { displayDialog.ShowErrorMessageBox(e.Message); }
            }
            return null;
        }

        public static BitmapImage ConvertBytesToBitmapImage(byte[] bytes)
        {
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }

        public static string GetRandomDollarValue(decimal min, decimal max)
        {
            const decimal factor = 100M;
            int minInt = Convert.ToInt32(min * factor);
            int maxInt = Convert.ToInt32(max * factor);
            return (random.Next(minInt, maxInt) / factor).ToString("C");
        }

        public static T Single<T>(this IEnumerable<T> enumerable, T value, IEqualityComparer<T> comparer)
        {
            return enumerable.Single(v => comparer.Equals(v, value));
        }

        public static bool Any<T>(this IEnumerable<T> enumerable, T value, IEqualityComparer<T> comparer)
        {
            return enumerable.Any(v => comparer.Equals(v, value));
        }
    }
}

using MediaKiosk.DisplayDialogs;
using MediaKiosk.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MediaKiosk
{
    public static class Utility
    {
        //Array which contains filters for png, jpg, jpeg, and gif files
        private static readonly string[] IMAGE_FILTERS =
        {
            "Image files (*.png, *.jpg, *.jpeg, *.gif, *.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp",
            "PNG Files (*.png)|*.png",
            "JPG Files (*.jpg)|*.jpg",
            "JPEG Files (*.jpeg)|*.jpeg",
            "GIF Files (*.gif)|*.gif",
            "BMP Files (*.bmp)|*.bmp"
        };
        
        //Combine all filters into one filter string
        public static readonly string ALL_IMAGE_FILTERS = string.Join("|", IMAGE_FILTERS);
        private static Random random = new Random();
        public static IDisplayDialog displayDialog;

        public static Microsoft.Win32.OpenFileDialog CreateImageFileDialog()
        {
            //Return new file browser dialog for all available image types
            return new Microsoft.Win32.OpenFileDialog()
            {
                CheckFileExists = true,
                CheckPathExists = true,
                AddExtension = true,
                Multiselect = false,
                Filter = Utility.ALL_IMAGE_FILTERS
            };
        }

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
    }
}

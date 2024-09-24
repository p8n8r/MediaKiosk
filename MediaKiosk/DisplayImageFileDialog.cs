using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MediaKiosk.DisplayDialogs
{
    public interface IDisplayImageFileDialog
    {
        string FilePath { get; }
        bool? OpenImageBrowser();
    }

    public class DisplayImageFileDialog : IDisplayImageFileDialog
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

        private OpenFileDialog dialog;
        public string FilePath { get { return dialog.FileName; } }

        public bool? OpenImageBrowser()
        {
            //Create file browser dialog
            this.dialog = new OpenFileDialog()
            {
                CheckFileExists = true,
                CheckPathExists = true,
                AddExtension = true,
                Multiselect = false,
                Filter = ALL_IMAGE_FILTERS
            };

            return this.dialog.ShowDialog();
        }
    }

    public class FakeDisplayImageFileDialog : IDisplayImageFileDialog
    {
        public string FilePath { get { return @".\Resources\sample.png"; } }

        public bool? OpenImageBrowser()
        {
            return true;
        }
    }
}

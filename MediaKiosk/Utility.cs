using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}

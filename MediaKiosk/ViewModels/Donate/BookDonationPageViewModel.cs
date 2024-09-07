using MediaKiosk.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MediaKiosk.ViewModels.Donate
{
    internal class BookDonationPageViewModel : ViewModelBase
    {
        public RelayCommand browseCmd => new RelayCommand(execute => BrowseForImage());
        private string title, author, category, publicationYear, coverArtFilePath;

        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged(); }
        }
        public string Author
        {
            get { return author; }
            set { author = value; OnPropertyChanged(); }
        }
        public string Category
        {
            get { return category; }
            set { category = value; OnPropertyChanged(); }
        }
        public string PublicationYear
        {
            get { return publicationYear; }
            set { publicationYear = value; OnPropertyChanged(); }
        }
        public string CoverArtFilePath
        {
            get { return coverArtFilePath; }
            set { coverArtFilePath = value; OnPropertyChanged(); }
        }

        private void BrowseForImage()
        {
            //Create file browser dialog
            OpenFileDialog dlg = Utility.CreateImageFileDialog();

            //Select image in dialog
            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                //Save image
                this.CoverArtFilePath = dlg.FileName;
                //this.CoverArt = new BitmapImage(new Uri(dlg.FileName));
            }
        }

        internal bool HasValidBookProperties()
        {
            if (string.IsNullOrWhiteSpace(this.Title))
                throw new InvalidBookException("Invalid title.");

            if (string.IsNullOrWhiteSpace(this.Author))
                throw new InvalidBookException("Invalid author.");

            if (string.IsNullOrWhiteSpace(this.Category))
                throw new InvalidBookException("Invalid category.");

            if (!int.TryParse(this.PublicationYear, out int pubYear)
                || (pubYear < 0 || pubYear > 2024))
                throw new InvalidBookException("Invalid publication year.");

            if (string.IsNullOrWhiteSpace(this.CoverArtFilePath) || !File.Exists(this.CoverArtFilePath))
                throw new InvalidBookException("Invalid cover art file.");

            return true;
        }

        internal void ClearBookProperties()
        {
            this.Title = string.Empty;
            this.Author = string.Empty;
            this.Category = string.Empty;
            this.PublicationYear = string.Empty;
            this.CoverArtFilePath = string.Empty;
        }
    }
}

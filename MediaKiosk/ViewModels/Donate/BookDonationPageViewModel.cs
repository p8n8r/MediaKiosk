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
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MediaKiosk.ViewModels.Donate
{
    internal class BookDonationPageViewModel : ViewModelBase
    {
        public RelayCommand browseCmd => new RelayCommand(execute => BrowseForImage());
        private string title, author, category, publicationYear, coverArtFilePath;
        private Brush titleColor;

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
        public Brush TitleBorderColor
        {
            get { return this.titleColor; }
            set { this.titleColor = value; OnPropertyChanged(); }
        }

        private void BrowseForImage()
        {
            //Create file browser dialog
            OpenFileDialog dlg = Utility.CreateImageFileDialog();

            //Select image in dialog
            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                //Save image file path
                this.CoverArtFilePath = dlg.FileName;
            }
        }

        internal bool HasValidBookProperties()
        {
            if (string.IsNullOrWhiteSpace(this.Title))
                throw new InvalidMediaException("Invalid title.", nameof(Book.Title));

            if (string.IsNullOrWhiteSpace(this.Author))
                throw new InvalidMediaException("Invalid author.");

            if (string.IsNullOrWhiteSpace(this.Category))
                throw new InvalidMediaException("Invalid category.");

            if (!int.TryParse(this.PublicationYear, out int pubYear)
                || (pubYear < 0 || pubYear > DateTime.Now.Year))
                throw new InvalidMediaException("Invalid publication year.");

            if (string.IsNullOrWhiteSpace(this.CoverArtFilePath) || !File.Exists(this.CoverArtFilePath))
                throw new InvalidMediaException("Invalid cover art file.");

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

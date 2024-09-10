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
    internal class MovieDonationPageViewModel : ViewModelBase
    {
        public RelayCommand browseCmd => new RelayCommand(execute => BrowseForImage());
        private string title, stars, category, releaseYear, promoArtFilePath;

        public string Title
        {
            get { return this.title; }
            set { this.title = value; OnPropertyChanged(); }
        }
        public string Rating
        {
            get { return this.stars; }
            set { this.stars = value; OnPropertyChanged(); }
        }
        public string Category
        {
            get { return this.category; }
            set { this.category = value; OnPropertyChanged(); }
        }
        public string ReleaseYear
        {
            get { return this.releaseYear; }
            set { this.releaseYear = value; OnPropertyChanged(); }
        }
        public string PromoArtFilePath
        {
            get { return this.promoArtFilePath; }
            set { this.promoArtFilePath = value; OnPropertyChanged(); }
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
                this.PromoArtFilePath = dlg.FileName;
            }
        }

        internal bool HasValidMovieProperties()
        {
            if (string.IsNullOrWhiteSpace(this.Title))
                throw new InvalidMediaException("Invalid title.");

            if (string.IsNullOrWhiteSpace(this.Rating))
                throw new InvalidMediaException("Invalid rating.");
            else
            {
                int starCnt = this.Rating.Count(c => c == Movie.STAR);
                if ((Rating)starCnt < Models.Rating.OneStar || (Rating)starCnt > Models.Rating.FiveStars)
                    throw new InvalidMediaException("Invalid rating.");
            }

            if (string.IsNullOrWhiteSpace(this.Category))
                throw new InvalidMediaException("Invalid category.");

            if (!int.TryParse(this.ReleaseYear, out int relYear)
                || (relYear < 0 || relYear > DateTime.Now.Year))
                throw new InvalidMediaException("Invalid release year.");

            if (string.IsNullOrWhiteSpace(this.PromoArtFilePath) || !File.Exists(this.PromoArtFilePath))
                throw new InvalidMediaException("Invalid promotional art file.");

            return true;
        }

        internal void ClearMovieProperties()
        {
            this.Title = string.Empty;
            this.Rating = string.Empty;
            this.Category = string.Empty;
            this.ReleaseYear = string.Empty;
            this.PromoArtFilePath = string.Empty;
        }
    }
}

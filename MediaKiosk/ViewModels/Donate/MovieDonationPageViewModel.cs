using MediaKiosk.DisplayDialogs;
using MediaKiosk.Models;
using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;

namespace MediaKiosk.ViewModels.Donate
{
    public class MovieDonationPageViewModel : ViewModelBase
    {
        private const int INIT_NUM_STARS = 1;
        public RelayCommand browseCmd => new RelayCommand(execute => BrowseForImage());
        public RelayCommand controlGotFocusCmd => new RelayCommand(sender => ResetBorder(sender));

        private string title, rating, category, releaseYear, promoArtFilePath;
        private Brush titleBorderBrush, ratingBorderBrush, categoryBorderBrush,
            releaseYearBorderBrush, promoArtFilePathBorderBrush;
        private readonly IDisplayImageFileDialog displayImageFileDialog;

        public string Title
        {
            get { return this.title; }
            set { this.title = value; OnPropertyChanged(); }
        }
        public string Rating
        {
            get { return this.rating; }
            set { this.rating = value; OnPropertyChanged(); }
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
        public Brush TitleBorderBrush
        {
            get { return this.titleBorderBrush; }
            set { this.titleBorderBrush = value; OnPropertyChanged(); }
        }
        public Brush RatingBorderBrush
        {
            get { return this.ratingBorderBrush; }
            set { this.ratingBorderBrush = value; OnPropertyChanged(); }
        }
        public Brush CategoryBorderBrush
        {
            get { return this.categoryBorderBrush; }
            set { this.categoryBorderBrush = value; OnPropertyChanged(); }
        }
        public Brush ReleaseYearBorderBrush
        {
            get { return this.releaseYearBorderBrush; }
            set { this.releaseYearBorderBrush = value; OnPropertyChanged(); }
        }
        public Brush PromoArtFilePathBorderBrush
        {
            get { return this.promoArtFilePathBorderBrush; }
            set { this.promoArtFilePathBorderBrush = value; OnPropertyChanged(); }
        }

        public MovieDonationPageViewModel()
        {
            this.displayImageFileDialog = new DisplayImageFileDialog();
            InitializeBorders();
        }

        private void InitializeBorders()
        {
            //Initialize rating
            this.Rating = new string(Movie.STAR, INIT_NUM_STARS);

            //Initialize all border colors
            this.TitleBorderBrush = Types.DEFAULT_BORDER_BRUSH;
            this.RatingBorderBrush = Types.DEFAULT_BORDER_BRUSH;
            this.CategoryBorderBrush = Types.DEFAULT_BORDER_BRUSH;
            this.ReleaseYearBorderBrush = Types.DEFAULT_BORDER_BRUSH;
            this.PromoArtFilePathBorderBrush = Types.DEFAULT_BORDER_BRUSH;
        }

        private void ResetBorder(object sender)
        {
            (sender as Control).BorderBrush = Types.DEFAULT_BORDER_BRUSH;
        }

        private void BrowseForImage()
        {
            //Select image in dialog
            bool? result = this.displayImageFileDialog.OpenImageBrowser();

            if (result == true)
            {
                //Save image file path
                this.PromoArtFilePath = this.displayImageFileDialog.FilePath;

                //Reset border color
                this.PromoArtFilePathBorderBrush = Types.DEFAULT_BORDER_BRUSH;
            }
        }

        public bool HasValidMovieProperties()
        {
            bool hasValidProperties = true;

            if (string.IsNullOrWhiteSpace(this.Title))
            {
                this.TitleBorderBrush = Types.INVALID_BORDER_BRUSH;
                hasValidProperties = false;
            }

            if (string.IsNullOrWhiteSpace(this.Rating))
            {
                this.RatingBorderBrush = Types.INVALID_BORDER_BRUSH;
                hasValidProperties = false;
            }

            if (string.IsNullOrWhiteSpace(this.Category))
            {
                this.CategoryBorderBrush = Types.INVALID_BORDER_BRUSH;
                hasValidProperties = false;
            }

            if (!int.TryParse(this.ReleaseYear, out int relYear)
                || (relYear < 0 || relYear > DateTime.Now.Year))
            {
                this.ReleaseYearBorderBrush = Types.INVALID_BORDER_BRUSH;
                hasValidProperties = false;
            }

            if (string.IsNullOrWhiteSpace(this.PromoArtFilePath)
                || !File.Exists(this.PromoArtFilePath))
            {
                this.PromoArtFilePathBorderBrush = Types.INVALID_BORDER_BRUSH;
                hasValidProperties = false;
            }

            return hasValidProperties;
        }

        public void ClearMovieProperties()
        {
            this.Title = string.Empty;
            this.Rating = string.Empty;
            this.Category = string.Empty;
            this.ReleaseYear = string.Empty;
            this.PromoArtFilePath = string.Empty;
        }
    }
}

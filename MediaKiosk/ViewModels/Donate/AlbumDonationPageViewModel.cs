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
using static MediaKiosk.Models.Album;

namespace MediaKiosk.ViewModels.Donate
{
    public class AlbumDonationPageViewModel : ViewModelBase
    {
        public RelayCommand browseCmd => new RelayCommand(execute => BrowseForImage());
        public RelayCommand controlGotFocusCmd => new RelayCommand(sender => ResetBorder(sender));
        
        private string title, artist, genre, releaseYear, albumArtFilePath;
        private Brush titleBorderBrush, artistBorderBrush, genreBorderBrush,
            releaseYearBorderBrush, albumArtFilePathBorderBrush;

        public string Title
        {
            get { return this.title; }
            set { this.title = value; OnPropertyChanged(); }
        }
        public string Artist
        {
            get { return this.artist; }
            set { this.artist = value; OnPropertyChanged(); }
        }
        public string Genre
        {
            get { return this.genre; }
            set { this.genre = value; OnPropertyChanged(); }
        }
        public string ReleaseYear
        {
            get { return this.releaseYear; }
            set { this.releaseYear = value; OnPropertyChanged(); }
        }
        public string AlbumArtFilePath
        {
            get { return this.albumArtFilePath; }
            set { this.albumArtFilePath = value; OnPropertyChanged(); }
        }
        public Brush TitleBorderBrush
        {
            get { return this.titleBorderBrush; }
            set { this.titleBorderBrush = value; OnPropertyChanged(); }
        }
        public Brush ArtistBorderBrush
        {
            get { return this.artistBorderBrush; }
            set { this.artistBorderBrush = value; OnPropertyChanged(); }
        }
        public Brush GenreBorderBrush
        {
            get { return this.genreBorderBrush; }
            set { this.genreBorderBrush = value; OnPropertyChanged(); }
        }
        public Brush ReleaseYearBorderBrush
        {
            get { return this.releaseYearBorderBrush; }
            set { this.releaseYearBorderBrush = value; OnPropertyChanged(); }
        }
        public Brush AlbumArtFilePathBorderBrush
        {
            get { return this.albumArtFilePathBorderBrush; }
            set { this.albumArtFilePathBorderBrush = value; OnPropertyChanged(); }
        }

        public AlbumDonationPageViewModel()
        {
            InitializeBorders();
        }

        private void InitializeBorders()
        {
            //Initialize all border colors
            this.TitleBorderBrush = Types.DEFAULT_BORDER_BRUSH;
            this.ArtistBorderBrush = Types.DEFAULT_BORDER_BRUSH;
            this.GenreBorderBrush = Types.DEFAULT_BORDER_BRUSH;
            this.ReleaseYearBorderBrush = Types.DEFAULT_BORDER_BRUSH;
            this.AlbumArtFilePathBorderBrush = Types.DEFAULT_BORDER_BRUSH;
        }

        private void ResetBorder(object sender)
        {
            (sender as Control).BorderBrush = Types.DEFAULT_BORDER_BRUSH;
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
                this.AlbumArtFilePath = dlg.FileName;

                //Reset border color
                this.AlbumArtFilePathBorderBrush = Types.DEFAULT_BORDER_BRUSH;
            }
        }

        public bool HasValidAlbumProperties()
        {
            bool hasValidProperties = true;

            if (string.IsNullOrWhiteSpace(this.Title))
            {
                this.TitleBorderBrush = Types.INVALID_BORDER_BRUSH;
                hasValidProperties = false;
            }

            if (string.IsNullOrWhiteSpace(this.Artist))
            {
                this.ArtistBorderBrush = Types.INVALID_BORDER_BRUSH;
                hasValidProperties = false;
            }

            if (string.IsNullOrWhiteSpace(this.Genre))
            {
                this.GenreBorderBrush = Types.INVALID_BORDER_BRUSH;
                hasValidProperties = false;
            }

            if (!int.TryParse(this.ReleaseYear, out int relYear)
                || (relYear < 0 || relYear > DateTime.Now.Year))
            {
                this.ReleaseYearBorderBrush = Types.INVALID_BORDER_BRUSH;
                hasValidProperties = false;
            }

            if (string.IsNullOrWhiteSpace(this.AlbumArtFilePath)
                || !File.Exists(this.AlbumArtFilePath))
            {
                this.AlbumArtFilePathBorderBrush = Types.INVALID_BORDER_BRUSH;
                hasValidProperties = false;
            }

            return hasValidProperties;
        }

        public void ClearAlbumProperties()
        {
            this.Title = string.Empty;
            this.Artist = string.Empty;
            this.Genre = string.Empty;
            this.ReleaseYear = string.Empty;
            this.AlbumArtFilePath = string.Empty;
        }
    }
}

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
using static MediaKiosk.Models.Album;

namespace MediaKiosk.ViewModels.Donate
{
    internal class AlbumDonationPageViewModel : ViewModelBase
    {
        public RelayCommand browseCmd => new RelayCommand(execute => BrowseForImage());
        private string title, artist, genre, releaseYear, albumArtFilePath;

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
            }
        }

        internal bool HasValidAlbumProperties()
        {
            if (string.IsNullOrWhiteSpace(this.Title))
                throw new InvalidMediaException("Invalid title.");

            if (string.IsNullOrWhiteSpace(this.Artist))
                throw new InvalidMediaException("Invalid artist.");

            if (string.IsNullOrWhiteSpace(this.Genre))
                throw new InvalidMediaException("Invalid genre.");

            if (!int.TryParse(this.ReleaseYear, out int relYear)
                || (relYear < 0 || relYear > DateTime.Now.Year))
                throw new InvalidMediaException("Invalid publication year.");

            if (string.IsNullOrWhiteSpace(this.AlbumArtFilePath) || !File.Exists(this.AlbumArtFilePath))
                throw new InvalidMediaException("Invalid album art file.");

            return true;
        }

        internal void ClearAlbumProperties()
        {
            this.Title = string.Empty;
            this.Artist = string.Empty;
            this.Genre = string.Empty;
            this.ReleaseYear = string.Empty;
            this.AlbumArtFilePath = string.Empty;
        }
    }
}

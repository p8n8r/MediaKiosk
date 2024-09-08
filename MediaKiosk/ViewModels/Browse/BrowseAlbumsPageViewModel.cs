using MediaKiosk.Models;
using MediaKiosk.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaKiosk.ViewModels.Browse
{
    internal class BrowseAlbumsPageViewModel : ViewModelBase
    {
        private MainWindow mainWindow;
        private Album selectedAlbum;
        private ObservableCollection<Album> albums;

        public Album SelectedAlbum
        {
            get { return this.selectedAlbum; }
            set { this.selectedAlbum = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Album> Albums
        {
            get { return this.albums; }
            set { this.albums = value; OnPropertyChanged(); }
        }

        public BrowseAlbumsPageViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            FillWithAlbums();
        }

        private void FillWithAlbums()
        {
            this.Albums = new ObservableCollection<Album>()
            {
                new Album() {
                    Title = "Four",
                    Artist = "Huey Lewis and the News",
                    Genre = "Rock",
                    Price = "$5.00",
                    Stock = 0
                },
                new Album() {
                    Title = "Thriller",
                    Artist = "Michael Jackson",
                    Genre = "Pop",
                    Price = "$2.00",
                    Stock = 3
                },
                new Album() {
                    Title = "Rio",
                    Artist = "Duran Duran",
                    Genre = "Pop",
                    Price = "$4.00",
                    Stock = 1
                }
            };
        }
    }
}

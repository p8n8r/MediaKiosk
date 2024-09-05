using MediaKiosk.Models;
using MediaKiosk.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaKiosk.ViewModels
{
    internal class BrowseAlbumsPageViewModel : ViewModelBase
    {
        private MainWindow mainWindow;
        private Album selectedAlbum;
        private ObservableCollection<Album> albums;

        public Album SelectedAlbum
        {
            get { return selectedAlbum; }
            set { this.selectedAlbum = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Album> Albums
        {
            get { return albums; }
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
                    Title = "gwerger",
                    Artist = "sdfgsdr",
                    Genre = "sfdgsdf",
                    Price = 5.00M,
                    Stock = 0
                },
                new Album() {
                    Title = "sfgsfdges",
                    Artist = "hrthrt",
                    Genre = "aergrg",
                    Price = 2.00M,
                    Stock = 3
                },
                new Album() {
                    Title = "htgerth",
                    Artist = "arger",
                    Genre = "w5gwe",
                    Price = 11.00M,
                    Stock = 1
                }
            };
        }
    }
}

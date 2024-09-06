using MediaKiosk.Models;
using MediaKiosk.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaKiosk.ViewModels
{
    internal class AlbumDetailsPageViewModel : ViewModelBase
    {
        private MainWindow mainWindow;
        private Album selectedAlbum;
        public Album SelectedAlbum
        {
            get { return this.selectedAlbum; }
            set { this.selectedAlbum = value; OnPropertyChanged(); }
        }

        public AlbumDetailsPageViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            this.SelectedAlbum = new Album();
        }

        internal void SetDetailsForDonations()
        {
            this.SelectedAlbum.Stock = 0;
            this.SelectedAlbum.Price = 0.00M;
            //TODO: disable textbox
        }
    }
}

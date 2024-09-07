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
        private Album selectedAlbum;
        public Album SelectedAlbum
        {
            get { return this.selectedAlbum; }
            set { this.selectedAlbum = value; OnPropertyChanged(); }
        }

        public AlbumDetailsPageViewModel()
        {
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

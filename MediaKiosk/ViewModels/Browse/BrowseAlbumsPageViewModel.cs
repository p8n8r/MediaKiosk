using MediaKiosk.Models;
using MediaKiosk.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MediaKiosk.ViewModels.Browse
{
    public class BrowseAlbumsPageViewModel : ViewModelBase
    {
        private Album selectedAlbum;
        private ObservableCollection<Album> albums;
        private MainWindowViewModel mainWindowViewModel;
        public RelayCommand reloadCmd => new RelayCommand(execute => ReloadAlbums());

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

        public BrowseAlbumsPageViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
        }

        private void ReloadAlbums()
        {
            this.Albums = new ObservableCollection<Album>(this.mainWindowViewModel.MediaLibrary.Albums);
            this.SelectedAlbum = this.Albums.FirstOrDefault() ?? null;
        }
    }
}

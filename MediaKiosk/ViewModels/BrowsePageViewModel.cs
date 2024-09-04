using MediaKiosk.Models;
using MediaKiosk.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MediaKiosk.ViewModels
{
    enum MediaType
    {
        Books, Magazines, Albums, Movies
    }

    internal class BrowsePageViewModel : ViewModelBase
    {
        private MainWindow mainWindow;
        private MediaType mediaType = MediaType.Books;
        internal BrowseBooksPageViewModel browseBooksPageViewModel;
        public RelayCommand buyCmd => new RelayCommand(execute => Buy(), canExecute => HasMadeSelection());
        public RelayCommand rentCmd => new RelayCommand(execute => Rent(), canExecute => HasMadeSelection());
        public RelayCommand selectBooksCmd => new RelayCommand(execute => { mediaType = MediaType.Books; });
        public RelayCommand selectAlbumsCmd => new RelayCommand(execute => { mediaType = MediaType.Albums; });
        public RelayCommand selectMoviesCmd => new RelayCommand(execute => { mediaType = MediaType.Movies; });

        public BrowsePageViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            this.browseBooksPageViewModel = this.mainWindow.browseBooksPage.DataContext as BrowseBooksPageViewModel;
        }

        public void Buy()
        {
            //TODO
        }

        public void Rent()
        {
            //TODO
        }

        public bool HasMadeSelection()
        {
            switch (this.mediaType)
            {
                case MediaType.Books:
                    return this.browseBooksPageViewModel.SelectedBook?.Stock >= 1;
                //case MediaType.Albums:
                //    return this.browseAlbumsPageViewModel.SelectedAlbum?.Stock >= 1;
                //case MediaType.Movies:
                //    return this.browseMoviesPageViewModel.SelectedMovie?.Stock >= 1;
                default:
                    return false; //Should never happen
            }
        }
    }
}

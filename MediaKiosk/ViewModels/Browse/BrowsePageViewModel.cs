using MediaKiosk.Models;
using MediaKiosk.Views;
using MediaKiosk.Views.Browse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MediaKiosk.ViewModels.Browse
{
    internal class BrowsePageViewModel : ViewModelBase
    {
        private const int EMPTY = 0;
        private MainWindow mainWindow;
        private MediaType mediaType = MediaType.Books;
        internal BrowseBooksPageViewModel browseBooksPageViewModel;
        internal BrowseAlbumsPageViewModel browseAlbumsPageViewModel;
        internal BrowseMoviesPageViewModel browseMoviesPageViewModel;
        public RelayCommand buyCmd => new RelayCommand(execute => Buy(), canExecute => HasMadeSelection());
        public RelayCommand rentCmd => new RelayCommand(execute => Rent(), canExecute => HasMadeSelection());
        public RelayCommand selectBooksCmd => new RelayCommand(execute => { SelectMediaType(MediaType.Books); });
        public RelayCommand selectAlbumsCmd => new RelayCommand(execute => { SelectMediaType(MediaType.Albums); });
        public RelayCommand selectMoviesCmd => new RelayCommand(execute => { SelectMediaType(MediaType.Movies); });

        public BrowsePageViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            this.browseBooksPageViewModel = this.mainWindow.browseBooksPage.DataContext as BrowseBooksPageViewModel;
            this.browseAlbumsPageViewModel = this.mainWindow.browseAlbumsPage.DataContext as BrowseAlbumsPageViewModel;
            this.browseMoviesPageViewModel = this.mainWindow.browseMoviesPage.DataContext as BrowseMoviesPageViewModel;
        }

        private void SelectMediaType(MediaType mediaType)
        {
            this.mediaType = mediaType;
            BrowsePage browsePage = this.mainWindow.browsePage;

            switch (this.mediaType)
            {
                case MediaType.Books:
                    browsePage.mediaTableFrame.Navigate(this.mainWindow.browseBooksPage);
                    break;
                case MediaType.Albums:
                    browsePage.mediaTableFrame.Navigate(this.mainWindow.browseAlbumsPage);
                    break;
                case MediaType.Movies:
                    browsePage.mediaTableFrame.Navigate(this.mainWindow.browseMoviesPage);
                    break;
            }
            
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
                    return this.browseBooksPageViewModel.SelectedBook?.Stock > EMPTY;
                case MediaType.Albums:
                    return this.browseAlbumsPageViewModel.SelectedAlbum?.Stock > EMPTY;
                case MediaType.Movies:
                    return this.browseMoviesPageViewModel.SelectedMovie?.Stock > EMPTY;
                default:
                    return false; //Should never happen
            }
        }
    }
}

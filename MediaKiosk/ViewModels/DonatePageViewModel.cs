using MediaKiosk.Models;
using MediaKiosk.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaKiosk.ViewModels
{
    internal class DonatePageViewModel : ViewModelBase
    {
        private MainWindow mainWindow;
        private MediaType mediaType = MediaType.Books;
        internal BrowseBooksPageViewModel browseBooksPageViewModel;
        internal BrowseAlbumsPageViewModel browseAlbumsPageViewModel;
        internal BrowseMoviesPageViewModel browseMoviesPageViewModel;
        public RelayCommand donateCmd => new RelayCommand(execute => Donate(), canExecute => IsMediaAcceptable());
        public RelayCommand selectBooksCmd => new RelayCommand(execute => { SelectMediaType(MediaType.Books); });
        public RelayCommand selectAlbumsCmd => new RelayCommand(execute => { SelectMediaType(MediaType.Albums); });
        public RelayCommand selectMoviesCmd => new RelayCommand(execute => { SelectMediaType(MediaType.Movies); });

        public DonatePageViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            this.browseBooksPageViewModel = this.mainWindow.browseBooksPage.DataContext as BrowseBooksPageViewModel;
            this.browseAlbumsPageViewModel = this.mainWindow.browseAlbumsPage.DataContext as BrowseAlbumsPageViewModel;
            this.browseMoviesPageViewModel = this.mainWindow.browseMoviesPage.DataContext as BrowseMoviesPageViewModel;
        }

        private void SelectMediaType(MediaType mediaType)
        {
            this.mediaType = mediaType;
            DonatePage donatePage = this.mainWindow.donatePage;

            switch (this.mediaType)
            {
                case MediaType.Books:
                    donatePage.mediaTableFrame.Navigate(this.mainWindow.browseBooksPage);
                    break;
                case MediaType.Albums:
                    donatePage.mediaTableFrame.Navigate(this.mainWindow.browseAlbumsPage);
                    break;
                case MediaType.Movies:
                    donatePage.mediaTableFrame.Navigate(this.mainWindow.browseMoviesPage);
                    break;
            }
        }

        public void Donate()
        {
            //TODO
        }

        public bool IsMediaAcceptable()
        {
            //TODO
            return false;
        }
    }
}

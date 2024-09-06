using MediaKiosk.Models;
using MediaKiosk.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MediaKiosk.ViewModels
{
    internal class DonatePageViewModel : ViewModelBase
    {
        private MainWindow mainWindow;
        private DonatePage donatePage;
        private Frame detailsFrame;
        private MediaType mediaType = MediaType.Books;
        private BookDetailsPage bookDetailsPage;
        private AlbumDetailsPage albumDetailsPage;
        private MovieDetailsPage movieDetailsPage;
        private BookDetailsPageViewModel bookDetailsPageViewModel;
        private AlbumDetailsPageViewModel albumDetailsPageViewModel;
        private MovieDetailsPageViewModel movieDetailsPageViewModel;
        public RelayCommand donateCmd => new RelayCommand(execute => Donate(), canExecute => IsMediaAcceptable());
        public RelayCommand selectBooksCmd => new RelayCommand(execute => { SelectMediaType(MediaType.Books); });
        public RelayCommand selectAlbumsCmd => new RelayCommand(execute => { SelectMediaType(MediaType.Albums); });
        public RelayCommand selectMoviesCmd => new RelayCommand(execute => { SelectMediaType(MediaType.Movies); });

        public DonatePageViewModel(DonatePage donatePage)
        {
            this.donatePage = donatePage;
            this.mainWindow = Application.Current.MainWindow as MainWindow;
            this.detailsFrame = donatePage.mediaTableFrame;

            //Construct pages and viewmodels
            this.bookDetailsPage = new BookDetailsPage(this.mainWindow);
            this.albumDetailsPage = new AlbumDetailsPage(this.mainWindow);
            this.movieDetailsPage = new MovieDetailsPage(this.mainWindow);

            this.bookDetailsPageViewModel = bookDetailsPage.DataContext as BookDetailsPageViewModel;
            this.albumDetailsPageViewModel = albumDetailsPage.DataContext as AlbumDetailsPageViewModel;
            this.movieDetailsPageViewModel = movieDetailsPage.DataContext as MovieDetailsPageViewModel;

            //Set initial navigation page
            this.detailsFrame.Navigate(this.bookDetailsPage);

            //Set specifics for donations
            SetPageDetailsForDonations();
        }

        private void SelectMediaType(MediaType mediaType)
        {
            this.mediaType = mediaType;

            switch (this.mediaType)
            {
                case MediaType.Books:
                    this.detailsFrame.Navigate(this.bookDetailsPage);
                    break;
                case MediaType.Albums:
                    this.detailsFrame.Navigate(this.albumDetailsPage);
                    break;
                case MediaType.Movies:
                    this.detailsFrame.Navigate(this.movieDetailsPage);
                    break;
            }
        }

        private void SetPageDetailsForDonations()
        {
            this.bookDetailsPageViewModel.SetDetailsForDonations();
            this.albumDetailsPageViewModel.SetDetailsForDonations();
            this.movieDetailsPageViewModel.SetDetailsForDonations();
            //this.SelectedBook.Stock = 0;
            //this.SelectedAlbum.Stock = 0;
            //this.SelectedMovie.Stock = 0;
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

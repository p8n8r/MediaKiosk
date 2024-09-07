using MediaKiosk.Models;
using MediaKiosk.Views;
using MediaKiosk.Views.Donate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MediaKiosk.ViewModels.Donate
{
    internal class DonatePageViewModel : ViewModelBase
    {
        private MainWindow mainWindow;
        private DonatePage donatePage;
        private Frame detailsFrame;
        private MediaType mediaType = MediaType.Books;
        private BookDonationPage bookDonationPage;
        //private AlbumDetailsPage albumDetailsPage;
        //private MovieDetailsPage movieDetailsPage;
        private BookDonationPageViewModel bookDonationPageViewModel;
        //private AlbumDetailsPageViewModel albumDetailsPageViewModel;
        //private MovieDetailsPageViewModel movieDetailsPageViewModel;
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
            this.bookDonationPage = new BookDonationPage();
            //this.albumDetailsPage = new AlbumDetailsPage();
            //this.movieDetailsPage = new MovieDetailsPage();

            this.bookDonationPageViewModel = bookDonationPage.DataContext as BookDonationPageViewModel;
            //this.albumDetailsPageViewModel = albumDetailsPage.DataContext as AlbumDetailsPageViewModel;
            //this.movieDetailsPageViewModel = movieDetailsPage.DataContext as MovieDetailsPageViewModel;

            //Set initial navigation page
            this.detailsFrame.Navigate(this.bookDonationPage);

            //Set specifics for donations
            SetPageDetailsForDonations();
        }

        private void SelectMediaType(MediaType mediaType)
        {
            this.mediaType = mediaType;

            switch (this.mediaType)
            {
                case MediaType.Books:
                    this.detailsFrame.Navigate(this.bookDonationPage);
                    break;
                //case MediaType.Albums:
                //    this.detailsFrame.Navigate(this.albumDetailsPage);
                //    break;
                //case MediaType.Movies:
                //    this.detailsFrame.Navigate(this.movieDetailsPage);
                //    break;
            }
        }

        private void SetPageDetailsForDonations()
        {
            //this.bookDonationPageViewModel.SetDetailsForDonations();
            //this.albumDetailsPageViewModel.SetDetailsForDonations();
            //this.movieDetailsPageViewModel.SetDetailsForDonations();
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
            switch (this.mediaType)
            {
                case MediaType.Books:
                    try
                    {
                        return bookDonationPageViewModel.HasValidBookProperties();
                    }
                    catch (InvalidBookException e)
                    {
                        return false;
                    }
                //case MediaType.Albums:
                //    return albumDonationPageViewModel.HasValidBookProperties();
                //case MediaType.Movies:
                //    return movieDonationPageViewModel.HasValidBookProperties();
                default:
                    return false;
            }
        }
    }
}

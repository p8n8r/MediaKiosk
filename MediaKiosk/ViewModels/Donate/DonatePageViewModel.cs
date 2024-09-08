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
using System.Windows.Media.Imaging;

namespace MediaKiosk.ViewModels.Donate
{
    internal class DonatePageViewModel : ViewModelBase
    {
        private readonly string[] THANKS_MESSAGES = 
        { 
            "Thanks for your donation!", 
            "Thank you for your generosity!", 
            "Thank you for donating!" 
        };
        private const decimal MIN_PRICE = 1.0M, MAX_PRICE = 10.0M;

        private MainWindowViewModel mainWindowViewModel;
        //private MainWindow mainWindow;
        internal MediaLibrary MediaLibrary { get; set; }
        private DonatePage donatePage;
        private Frame detailsFrame;
        private MediaType mediaType = MediaType.Books;
        private BookDonationPage bookDonationPage;
        //private AlbumDetailsPage albumDetailsPage;
        //private MovieDetailsPage movieDetailsPage;
        private BookDonationPageViewModel bookDonationPageViewModel;
        //private AlbumDetailsPageViewModel albumDetailsPageViewModel;
        //private MovieDetailsPageViewModel movieDetailsPageViewModel;
        private List<Book> books = new List<Book>();
        private Random random = new Random();
        public RelayCommand donateCmd => new RelayCommand(execute => Donate(), canExecute => IsMediaAcceptable());
        public RelayCommand selectBooksCmd => new RelayCommand(execute => { SelectMediaType(MediaType.Books); });
        public RelayCommand selectAlbumsCmd => new RelayCommand(execute => { SelectMediaType(MediaType.Albums); });
        public RelayCommand selectMoviesCmd => new RelayCommand(execute => { SelectMediaType(MediaType.Movies); });

        public DonatePageViewModel(DonatePage donatePage)
        {
            this.donatePage = donatePage;
            this.detailsFrame = donatePage.mediaTableFrame;
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            this.mainWindowViewModel = mainWindow.DataContext as MainWindowViewModel;
            this.MediaLibrary = this.mainWindowViewModel.MediaLibrary;

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
            //All properties of selected media type should be valid now
            switch (this.mediaType)
            {
                case MediaType.Books:
                    BitmapImage artwork = new BitmapImage(new Uri(bookDonationPageViewModel.CoverArtFilePath)); //TODO: Make relative

                    Book book = new Book()
                    {
                        Title = bookDonationPageViewModel.Title,
                        Author = bookDonationPageViewModel.Author,
                        Category = bookDonationPageViewModel.Category,
                        PublicationYear = Convert.ToInt32(bookDonationPageViewModel.PublicationYear),
                        ArtWork = artwork,
                        ArtWorkBytes = Utility.ConvertBitmapImageToBytes(artwork)
                    };

                    //has book already?
                    //  add one to stock
                    //else
                    book.Stock = 1;
                    book.Price = GetRandomPrice();
                    this.MediaLibrary.Books.Add(book);

                    //Thank user for donation via messagebox
                    MessageBox.Show(THANKS_MESSAGES[random.Next(THANKS_MESSAGES.Count())]);
                    bookDonationPageViewModel.ClearBookProperties();
                    break;
                //case MediaType.Albums:
                //    ...
                //case MediaType.Movies:
                //    ...
            }
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

        public static string GetRandomPrice()
        {
            return Utility.GetRandomDollarValue(MIN_PRICE, MAX_PRICE);
        }
    }
}

using MediaKiosk.DisplayDialogs;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MediaKiosk.ViewModels.Donate
{
    public class DonatePageViewModel : ViewModelBase
    {
        private readonly string[] THANKS_MESSAGES = 
        { 
            "Thanks for your donation!", 
            "Thank you for your generosity!", 
            "We appreciate you contribution!",
            "Thank you for donating!" 
        };
        private const decimal MIN_PRICE = 1.0M, MAX_PRICE = 10.0M;
        private readonly IDisplayDialog displayDialog;

        private MainWindow mainWindow;
        private MainWindowViewModel mainWindowViewModel;
        public MediaLibrary MediaLibrary { get; set; }
        private Frame detailsFrame;
        private MediaType mediaType = MediaType.Books;
        public BookDonationPage bookDonationPage;
        public AlbumDonationPage albumDonationPage;
        public MovieDonationPage movieDonationPage;
        private BookDonationPageViewModel bookDonationPageViewModel;
        private AlbumDonationPageViewModel albumDonationPageViewModel;
        private MovieDonationPageViewModel movieDonationPageViewModel;
        private BookComparer bookComparer = new BookComparer();
        private AlbumComparer albumComparer = new AlbumComparer();
        private MovieComparer movieComparer = new MovieComparer();
        private Random random = new Random();
        public RelayCommand donateCmd => new RelayCommand(execute => Donate());
        public RelayCommand selectBooksCmd => new RelayCommand(execute => { SelectMediaType(MediaType.Books); });
        public RelayCommand selectAlbumsCmd => new RelayCommand(execute => { SelectMediaType(MediaType.Albums); });
        public RelayCommand selectMoviesCmd => new RelayCommand(execute => { SelectMediaType(MediaType.Movies); });

        public DonatePageViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            this.mainWindow = mainWindowViewModel.MainWindow;
            this.displayDialog = this.mainWindowViewModel.displayDialog;
            this.MediaLibrary = this.mainWindowViewModel.MediaLibrary;

            //Construct pages
            this.bookDonationPage = new BookDonationPage();
            this.albumDonationPage = new AlbumDonationPage();
            this.movieDonationPage = new MovieDonationPage();

            //Capture viewmodels
            this.bookDonationPageViewModel = bookDonationPage.DataContext as BookDonationPageViewModel;
            this.albumDonationPageViewModel = albumDonationPage.DataContext as AlbumDonationPageViewModel;
            this.movieDonationPageViewModel = movieDonationPage.DataContext as MovieDonationPageViewModel;
        }

        private void SelectMediaType(MediaType mediaType)
        {
            this.mediaType = mediaType;
            this.detailsFrame = this.mainWindow.donatePage.mediaTableFrame; //replace with binding?

            switch (this.mediaType)
            {
                case MediaType.Books:
                    this.detailsFrame.Navigate(this.bookDonationPage);
                    break;
                case MediaType.Albums:
                    this.detailsFrame.Navigate(this.albumDonationPage);
                    break;
                case MediaType.Movies:
                    this.detailsFrame.Navigate(this.movieDonationPage);
                    break;
            }
        }

        public void Donate()
        {
            if (!IsMediaAcceptable()) //Showsinvalid controls
                return;

            //All properties of selected media type should be valid now
            switch (this.mediaType)
            {
                case MediaType.Books:
                    BitmapImage coverArt = new BitmapImage(
                        new Uri(this.bookDonationPageViewModel.CoverArtFilePath)); 

                    Book book = new Book()
                    {
                        Title = this.bookDonationPageViewModel.Title,
                        Author = this.bookDonationPageViewModel.Author,
                        Category = this.bookDonationPageViewModel.Category,
                        PublicationYear = Convert.ToInt32(this.bookDonationPageViewModel.PublicationYear),
                        ArtWork = coverArt,
                        ArtWorkBytes = Utility.ConvertBitmapImageToBytes(coverArt)
                    };

                    if (this.MediaLibrary.Books.Contains(book, bookComparer))
                    {
                        Book bookSame = this.MediaLibrary.Books.Single(book, bookComparer);
                        bookSame.Stock++;
                    }
                    else
                    {
                        book.Stock = 1;
                        book.Price = GetRandomPrice();
                        this.MediaLibrary.Books.Add(book);
                    }

                    this.bookDonationPageViewModel.ClearBookProperties();
                    this.mainWindowViewModel.MediaLibrary.SortBooksByTitle();
                    break;

                case MediaType.Albums:
                    BitmapImage albumArtwork = new BitmapImage(
                        new Uri(this.albumDonationPageViewModel.AlbumArtFilePath)); 

                    Album album = new Album()
                    {
                        Title = this.albumDonationPageViewModel.Title,
                        Artist = this.albumDonationPageViewModel.Artist,
                        Genre = this.albumDonationPageViewModel.Genre,
                        ReleaseYear = Convert.ToInt32(this.albumDonationPageViewModel.ReleaseYear),
                        ArtWork = albumArtwork,
                        ArtWorkBytes = Utility.ConvertBitmapImageToBytes(albumArtwork)
                    };

                    if (this.MediaLibrary.Albums.Contains(album, albumComparer))
                    {
                        Album albumSame = this.MediaLibrary.Albums.Single(album, albumComparer);
                        albumSame.Stock++;
                    }
                    else
                    {
                        album.Stock = 1;
                        album.Price = GetRandomPrice();
                        this.MediaLibrary.Albums.Add(album);
                    }

                    this.albumDonationPageViewModel.ClearAlbumProperties();
                    this.mainWindowViewModel.MediaLibrary.SortAlbumsByTitle();
                    break;

                case MediaType.Movies:
                    BitmapImage promoArtwork = new BitmapImage(
                        new Uri(this.movieDonationPageViewModel.PromoArtFilePath)); 

                    Movie movie = new Movie()
                    {
                        Title = this.movieDonationPageViewModel.Title,
                        Rating = this.movieDonationPageViewModel.Rating,
                        Category = this.movieDonationPageViewModel.Category,
                        ReleaseYear = Convert.ToInt32(this.movieDonationPageViewModel.ReleaseYear),
                        ArtWork = promoArtwork,
                        ArtWorkBytes = Utility.ConvertBitmapImageToBytes(promoArtwork)
                    };

                    if (this.MediaLibrary.Movies.Contains(movie, movieComparer))
                    {
                        Movie movieSame = this.MediaLibrary.Movies.Single(movie, movieComparer);
                        movieSame.Stock++;
                    }
                    else
                    {
                        movie.Stock = 1;
                        movie.Price = GetRandomPrice();
                        this.MediaLibrary.Movies.Add(movie);
                    }

                    this.movieDonationPageViewModel.ClearMovieProperties();
                    this.mainWindowViewModel.MediaLibrary.SortMoviesByTitle();
                    break;
            }

            //Thank user for donation via messagebox
            displayDialog.ShowBasicMessageBox(THANKS_MESSAGES[random.Next(THANKS_MESSAGES.Count())]);
        }

        public bool IsMediaAcceptable()
        {
            switch (this.mediaType)
            {
                case MediaType.Books:
                    return this.bookDonationPageViewModel.HasValidBookProperties();
                case MediaType.Albums:
                    return this.albumDonationPageViewModel.HasValidAlbumProperties();
                case MediaType.Movies:
                    return this.movieDonationPageViewModel.HasValidMovieProperties();
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

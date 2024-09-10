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
    internal class DonatePageViewModel : ViewModelBase
    {
        private readonly string[] THANKS_MESSAGES = 
        { 
            "Thanks for your donation!", 
            "Thank you for your generosity!", 
            "Thank you for donating!" 
        };
        private const decimal MIN_PRICE = 1.0M, MAX_PRICE = 10.0M;
        private static readonly Color DEFAULT_BORDER_COLOR = Color.FromRgb(171, 173, 179);
        private static readonly Brush DEFAULT_BORDER_BRUSH = new SolidColorBrush(DEFAULT_BORDER_COLOR);

        private MainWindowViewModel mainWindowViewModel;
        //private MainWindow mainWindow;
        internal MediaLibrary MediaLibrary { get; set; }
        private DonatePage donatePage;
        private Frame detailsFrame;
        private MediaType mediaType = MediaType.Books;
        private BookDonationPage bookDonationPage;
        private AlbumDonationPage albumDonationPage;
        private MovieDonationPage movieDonationPage;
        private BookDonationPageViewModel bookDonationPageViewModel;
        private AlbumDonationPageViewModel albumDonationPageViewModel;
        private MovieDonationPageViewModel movieDonationPageViewModel;
        private BookComparer bookComparer = new BookComparer();
        private AlbumComparer albumComparer = new AlbumComparer();
        private MovieComparer movieComparer = new MovieComparer();
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
            this.albumDonationPage = new AlbumDonationPage();
            this.movieDonationPage = new MovieDonationPage();

            this.bookDonationPageViewModel = bookDonationPage.DataContext as BookDonationPageViewModel;
            this.albumDonationPageViewModel = albumDonationPage.DataContext as AlbumDonationPageViewModel;
            this.movieDonationPageViewModel = movieDonationPage.DataContext as MovieDonationPageViewModel;

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
                case MediaType.Albums:
                    this.detailsFrame.Navigate(this.albumDonationPage);
                    break;
                case MediaType.Movies:
                    this.detailsFrame.Navigate(this.movieDonationPage);
                    break;
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
                    BitmapImage coverArt = new BitmapImage(
                        new Uri(this.bookDonationPageViewModel.CoverArtFilePath)); //TODO: Make relative

                    Book book = new Book()
                    {
                        Title = bookDonationPageViewModel.Title,
                        Author = bookDonationPageViewModel.Author,
                        Category = bookDonationPageViewModel.Category,
                        PublicationYear = Convert.ToInt32(bookDonationPageViewModel.PublicationYear),
                        ArtWork = coverArt,
                        ArtWorkBytes = Utility.ConvertBitmapImageToBytes(coverArt)
                    };

                    if (this.MediaLibrary.Books.Contains(book, bookComparer))
                    {
                        Book bookSame = this.MediaLibrary.Books.Single(bookComparer, book);
                        bookSame.Stock++;
                    }
                    else
                    {
                        book.Stock = 1;
                        book.Price = GetRandomPrice();
                        this.MediaLibrary.Books.Add(book);
                    }

                    bookDonationPageViewModel.ClearBookProperties();
                    break;

                case MediaType.Albums:
                    BitmapImage albumArtwork = new BitmapImage(
                        new Uri(this.albumDonationPageViewModel.AlbumArtFilePath)); //TODO: Make relative

                    Album album = new Album()
                    {
                        Title = albumDonationPageViewModel.Title,
                        Artist = albumDonationPageViewModel.Artist,
                        Genre = albumDonationPageViewModel.Genre,
                        ReleaseYear = Convert.ToInt32(albumDonationPageViewModel.ReleaseYear),
                        ArtWork = albumArtwork,
                        ArtWorkBytes = Utility.ConvertBitmapImageToBytes(albumArtwork)
                    };

                    if (this.MediaLibrary.Albums.Contains(album, albumComparer))
                    {
                        Album albumSame = this.MediaLibrary.Albums.Single(albumComparer, album);
                        albumSame.Stock++;
                    }
                    else
                    {
                        album.Stock = 1;
                        album.Price = GetRandomPrice();
                        this.MediaLibrary.Albums.Add(album);
                    }

                    albumDonationPageViewModel.ClearAlbumProperties();
                    break;

                case MediaType.Movies:
                    BitmapImage promoArtwork = new BitmapImage(
                        new Uri(this.movieDonationPageViewModel.PromoArtFilePath)); //TODO: Make relative

                    Movie movie = new Movie()
                    {
                        Title = movieDonationPageViewModel.Title,
                        Rating = movieDonationPageViewModel.Rating,
                        Category = movieDonationPageViewModel.Category,
                        ReleaseYear = Convert.ToInt32(movieDonationPageViewModel.ReleaseYear),
                        ArtWork = promoArtwork,
                        ArtWorkBytes = Utility.ConvertBitmapImageToBytes(promoArtwork)
                    };

                    if (this.MediaLibrary.Movies.Contains(movie, movieComparer))
                    {
                        Movie movieSame = this.MediaLibrary.Movies.Single(movieComparer, movie);
                        movieSame.Stock++;
                    }
                    else
                    {
                        movie.Stock = 1;
                        movie.Price = GetRandomPrice();
                        this.MediaLibrary.Movies.Add(movie);
                    }

                    movieDonationPageViewModel.ClearMovieProperties();
                    break;
            }

            //Thank user for donation via messagebox
            MessageBox.Show(THANKS_MESSAGES[random.Next(THANKS_MESSAGES.Count())]);
        }

        public bool IsMediaAcceptable()
        {
            //Set borders back to default colors
            this.bookDonationPageViewModel.TitleBorderColor = DEFAULT_BORDER_BRUSH;

            try
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
            catch (InvalidMediaException e)
            {
                switch (e.Property)
                {
                    case nameof(Book.Title):
                        this.bookDonationPageViewModel.TitleBorderColor = Brushes.Red;
                        break;
                }

                return false;
            } //TODO: Reveal bad details
        }

        public static string GetRandomPrice()
        {
            return Utility.GetRandomDollarValue(MIN_PRICE, MAX_PRICE);
        }
    }
}

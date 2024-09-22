using MediaKiosk.DisplayDialogs;
using MediaKiosk.Models;
using MediaKiosk.Views;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MediaKiosk.ViewModels.Returns
{
    public class ReturnsPageViewModel : ViewModelBase
    {
        private static readonly string[] THANKS_FOR_RETURN =
        {
            "Thanks for returning that!",
            "Thank you for bringing that back!",
            "Thanks! We hope you enjoyed it."
        };
        private readonly IDisplayDialog displayDialog;

        private MainWindowViewModel mainWindowViewModel;
        private Media selectedPurchasedMedia, selectedRentedMedia;
        private ObservableCollection<Media> purchasedMedia, rentedMedia;
        private BookComparer bookComparer = new BookComparer();
        private AlbumComparer albumComparer = new AlbumComparer();
        private MovieComparer movieComparer = new MovieComparer();
        private Random random = new Random();
        public RelayCommand returnCmd => new RelayCommand(media => Return(media), 
            media => this.SelectedRentedMedia != null);
        public RelayCommand reloadCmd => new RelayCommand(execute => ReloadMedia());

        public Media SelectedPurchasedMedia
        {
            get { return this.selectedPurchasedMedia; }
            set { this.selectedPurchasedMedia = value; OnPropertyChanged(); }
        }
        public Media SelectedRentedMedia
        {
            get { return this.selectedRentedMedia; }
            set { this.selectedRentedMedia = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Media> PurchasedMedia
        {
            get { return this.purchasedMedia; }
            set { this.purchasedMedia = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Media> RentedMedia
        {
            get { return this.rentedMedia; }
            set { this.rentedMedia = value; OnPropertyChanged(); }
        }

        public ReturnsPageViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            this.displayDialog = this.mainWindowViewModel.displayDialog;
        }

        private void ReloadMedia()
        {
            this.PurchasedMedia = CompileMedia(this.mainWindowViewModel.CurrentUser.Purchases);
            this.RentedMedia = CompileMedia(this.mainWindowViewModel.CurrentUser.Rentals);
        }

        private ObservableCollection<Media> CompileMedia(MediaLibrary mediaLibrary)
        {
            ObservableCollection<Media> allMedia = new ObservableCollection<Media>();
            foreach (Book book in mediaLibrary.Books)
                allMedia.Add(book);
            foreach (Album album in mediaLibrary.Albums)
                allMedia.Add(album);
            foreach (Movie movie in mediaLibrary.Movies)
                allMedia.Add(movie);
            return allMedia;
        }

        private void Return(object mediaObj)
        {
            Media media = mediaObj as Media;

            //Add media to library
            if (media.GetType() == typeof(Book))
            {
                Book book = media as Book;

                //Check library
                if (this.mainWindowViewModel.MediaLibrary.Books.Contains(book, bookComparer))
                {
                    Book bookSame = this.mainWindowViewModel.MediaLibrary.Books.Single(book, bookComparer);
                    bookSame.Stock++;
                }
                else
                {
                    book.Stock = 1;
                    this.mainWindowViewModel.MediaLibrary.Books.Add(book);
                }

                //Check returns
                if (this.mainWindowViewModel.CurrentUser.Rentals.Books.Any(book, bookComparer))
                {
                    Book bookSame = this.mainWindowViewModel.CurrentUser.Rentals.Books.Single(book, bookComparer);
                    bookSame.Stock--;

                    if (bookSame.Stock <= Types.EMPTY_STOCK)
                        this.mainWindowViewModel.CurrentUser.Rentals.Books.Remove(book);
                }
            }
            else if (media.GetType() == typeof(Album))
            {
                Album album = media as Album;

                //Check library
                if (this.mainWindowViewModel.MediaLibrary.Albums.Contains(album, albumComparer))
                {
                    Album albumSame = this.mainWindowViewModel.MediaLibrary.Albums.Single(album, albumComparer);
                    albumSame.Stock++;
                }
                else
                {
                    album.Stock = 1;
                    this.mainWindowViewModel.MediaLibrary.Albums.Add(album);
                }

                //Check returns
                if (this.mainWindowViewModel.CurrentUser.Rentals.Albums.Any(album, albumComparer))
                {
                    Album albumSame = this.mainWindowViewModel.CurrentUser.Rentals.Albums.Single(album, albumComparer);
                    albumSame.Stock--;

                    if (albumSame.Stock <= Types.EMPTY_STOCK)
                        this.mainWindowViewModel.CurrentUser.Rentals.Albums.Remove(album);
                }
            }
            else if (media.GetType() == typeof(Movie))
            {
                Movie movie = media as Movie;

                //Check library
                if (this.mainWindowViewModel.MediaLibrary.Movies.Contains(movie, movieComparer))
                {
                    Movie movieSame = this.mainWindowViewModel.MediaLibrary.Movies.Single(movie, movieComparer);
                    movieSame.Stock++;
                }
                else
                {
                    movie.Stock = 1;
                    this.mainWindowViewModel.MediaLibrary.Movies.Add(movie);
                }

                //Check returns
                if (this.mainWindowViewModel.CurrentUser.Rentals.Movies.Any(movie, movieComparer))
                {
                    Movie movieSame = this.mainWindowViewModel.CurrentUser.Rentals.Movies.Single(movie, movieComparer);
                    movieSame.Stock--;

                    if (movieSame.Stock <= Types.EMPTY_STOCK)
                        this.mainWindowViewModel.CurrentUser.Rentals.Movies.Remove(movie);
                }
            }

            //Remove media from pending returns
            if (media.Stock <= Types.EMPTY_STOCK)
                this.RentedMedia.Remove(media);

            //Force refresh of media in returns page,
            //so Media subclasses can remain plain old CLR objects (pocos).
            CollectionViewSource.GetDefaultView(this.RentedMedia).Refresh();

            displayDialog.ShowBasicMessageBox(THANKS_FOR_RETURN[random.Next(THANKS_FOR_RETURN.Count())]);
        }
    }
}

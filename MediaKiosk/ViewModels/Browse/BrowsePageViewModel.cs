using MediaKiosk.Models;
using MediaKiosk.Views;
using MediaKiosk.Views.Browse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MediaKiosk.ViewModels.Browse
{
    internal class BrowsePageViewModel : ViewModelBase
    {
        private static readonly string[] THANKS_FOR_PURCHASE_MESSAGES =
        {
            "Thank you for your patronage!",
            "Thanks for your purchase!",
            "We appreciate your business!",
        };
        private static readonly string[] THANKS_FOR_RENT_MESSAGES =
        {
            "Thanks for using the kiosk!",
            "Thanks! Don't forget to return that.",
            "Thank you! Please remember to return your media.",
        };
        private const int EMPTY = 0;

        private MainWindow mainWindow;
        private MainWindowViewModel mainWindowViewModel;
        private MediaType mediaType = MediaType.Books;
        private Random random = new Random();
        internal BrowseBooksPage browseBooksPage;
        internal BrowseAlbumsPage browseAlbumsPage;
        internal BrowseMoviesPage browseMoviesPage;
        private BrowseBooksPageViewModel browseBooksPageViewModel;
        private BrowseAlbumsPageViewModel browseAlbumsPageViewModel;
        private BrowseMoviesPageViewModel browseMoviesPageViewModel;
        private BookComparer bookComparer = new BookComparer();
        private AlbumComparer albumComparer = new AlbumComparer();
        private MovieComparer movieComparer = new MovieComparer();
        public RelayCommand buyCmd => new RelayCommand(execute => Buy(), canExecute => HasMadeSelection());
        public RelayCommand rentCmd => new RelayCommand(execute => Rent(), canExecute => HasMadeSelection());
        public RelayCommand selectBooksCmd => new RelayCommand(execute => { SelectMediaType(MediaType.Books); });
        public RelayCommand selectAlbumsCmd => new RelayCommand(execute => { SelectMediaType(MediaType.Albums); });
        public RelayCommand selectMoviesCmd => new RelayCommand(execute => { SelectMediaType(MediaType.Movies); });

        public BrowsePageViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            this.mainWindow = this.mainWindowViewModel.MainWindow;

            //Construct pages
            this.browseBooksPage = new BrowseBooksPage(mainWindowViewModel);
            this.browseAlbumsPage = new BrowseAlbumsPage(mainWindowViewModel);
            this.browseMoviesPage = new BrowseMoviesPage(mainWindowViewModel);

            //Capture viewmodels
            this.browseBooksPageViewModel = browseBooksPage.DataContext as BrowseBooksPageViewModel;
            this.browseAlbumsPageViewModel = browseAlbumsPage.DataContext as BrowseAlbumsPageViewModel;
            this.browseMoviesPageViewModel = browseMoviesPage.DataContext as BrowseMoviesPageViewModel;
        }

        private void SelectMediaType(MediaType mediaType)
        {
            this.mediaType = mediaType;
            BrowsePage browsePage = this.mainWindow.browsePage;

            switch (this.mediaType)
            {
                case MediaType.Books:
                    browsePage.mediaTableFrame.Navigate(this.browseBooksPage);
                    break;
                case MediaType.Albums:
                    browsePage.mediaTableFrame.Navigate(this.browseAlbumsPage);
                    break;
                case MediaType.Movies:
                    browsePage.mediaTableFrame.Navigate(this.browseMoviesPage);
                    break;
            }
        }

        public void Buy()
        {
            Checkout(this.mainWindowViewModel.CurrentUser.Purchases);

            //Thank user for purchase via messagebox
            MessageBox.Show(THANKS_FOR_PURCHASE_MESSAGES[random.Next(THANKS_FOR_PURCHASE_MESSAGES.Count())]);
        }

        public void Rent()
        {
            Checkout(this.mainWindowViewModel.CurrentUser.Rentals);

            //Thank user for renting via messagebox
            MessageBox.Show(THANKS_FOR_RENT_MESSAGES[random.Next(THANKS_FOR_RENT_MESSAGES.Count())]);
        }

        //Consider refactoring to be DRY
        private void Checkout(MediaLibrary mediaLibrary)
        {
            switch (this.mediaType)
            {
                case MediaType.Books:
                    //Subtract one from stock
                    Book selectedBook = this.browseBooksPageViewModel.SelectedBook;
                    selectedBook.Stock--;

                    if (selectedBook.Stock <= 0)
                        this.browseBooksPageViewModel.Books.Remove(selectedBook);

                    //Force refresh of media in browse page,
                    //so Media subclasses can remain plain old CLR objects (pocos).
                    CollectionViewSource.GetDefaultView(this.browseBooksPageViewModel.Books).Refresh();
                    BindingOperations.GetBindingExpression(this.browseBooksPage.stockTextBox,
                        TextBox.TextProperty).UpdateTarget();

                    if (mediaLibrary.Books.Contains(selectedBook, bookComparer))
                    {
                        Book bookSame = mediaLibrary.Books.Single(selectedBook, bookComparer);
                        bookSame.Stock++;
                    }
                    else
                    {
                        Book purchasedBook = (Book)selectedBook.Clone();
                        purchasedBook.Stock = 1;
                        mediaLibrary.Books.Add(purchasedBook);
                    }
                    break;

                case MediaType.Albums:
                    //Subtract one from stock
                    Album selectedAlbum = this.browseAlbumsPageViewModel.SelectedAlbum;
                    selectedAlbum.Stock--;

                    if (selectedAlbum.Stock <= 0)
                        this.browseAlbumsPageViewModel.Albums.Remove(selectedAlbum);

                    //Force refresh of media in browse page,
                    //so Media subclasses can remain plain old CLR objects (pocos).
                    CollectionViewSource.GetDefaultView(this.browseAlbumsPageViewModel.Albums).Refresh();
                    BindingOperations.GetBindingExpression(this.browseAlbumsPage.stockTextBox,
                        TextBox.TextProperty).UpdateTarget();

                    if (mediaLibrary.Albums.Contains(selectedAlbum, albumComparer))
                    {
                        Album albumSame = mediaLibrary.Albums.Single(selectedAlbum, albumComparer);
                        albumSame.Stock++;
                    }
                    else
                    {
                        Album purchasedAlbum = (Album)selectedAlbum.Clone();
                        purchasedAlbum.Stock = 1;
                        mediaLibrary.Albums.Add(purchasedAlbum);
                    }
                    break;

                case MediaType.Movies:
                    //Subtract one from stock
                    Movie selectedMovie = this.browseMoviesPageViewModel.SelectedMovie;
                    selectedMovie.Stock--;

                    if (selectedMovie.Stock <= 0)
                        this.browseMoviesPageViewModel.Movies.Remove(selectedMovie);

                    //Force refresh of media in browse page,
                    //so Media subclasses can remain plain old CLR objects (pocos).
                    CollectionViewSource.GetDefaultView(this.browseMoviesPageViewModel.Movies).Refresh();
                    BindingOperations.GetBindingExpression(this.browseMoviesPage.stockTextBox,
                        TextBox.TextProperty).UpdateTarget();

                    if (mediaLibrary.Movies.Contains(selectedMovie, movieComparer))
                    {
                        Movie movieSame = mediaLibrary.Movies.Single(selectedMovie, movieComparer);
                        movieSame.Stock++;
                    }
                    else
                    {
                        Movie purchasedMovie = (Movie)selectedMovie.Clone();
                        purchasedMovie.Stock = 1;
                        mediaLibrary.Movies.Add(purchasedMovie);
                    }
                    break;
            }
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

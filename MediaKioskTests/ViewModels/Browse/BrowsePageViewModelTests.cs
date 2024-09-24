using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaKiosk.ViewModels.Browse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaKiosk.Views;
using MediaKiosk.Models;
using MediaKiosk.Views.Browse;
using System.Windows.Controls;
using MediaKiosk.DisplayDialogs;
using System.Windows.Navigation;

namespace MediaKiosk.ViewModels.Browse.Tests
{
    [TestClass()]
    public class BrowsePageViewModelTests
    {
        [TestMethod()]
        public void BrowsePageViewModelTest()
        {
            MainWindow mainWindow = new MainWindow();
            MainWindowViewModel mainWindowVM = mainWindow.DataContext as MainWindowViewModel;
            BrowsePageViewModel browsePageVM = mainWindow.browsePage.DataContext as BrowsePageViewModel;

            Assert.IsNotNull(mainWindowVM);
            Assert.IsNotNull(browsePageVM);
            Assert.IsNotNull(mainWindow.displayDialog);
            Assert.IsNotNull(browsePageVM.browseBooksPage);
            Assert.IsNotNull(browsePageVM.browseAlbumsPage);
            Assert.IsNotNull(browsePageVM.browseMoviesPage);
        }

        [TestMethod()]
        public void SelectMediaTypeTest()
        {
            IDisplayDialog fakeDisplayDialog = new FakeDisplayDialog();
            MainWindow mainWindow = new MainWindow(fakeDisplayDialog);
            MainWindowViewModel mainWindowVM = mainWindow.DataContext as MainWindowViewModel;
            BrowsePage browsePage = mainWindow.browsePage;
            BrowsePageViewModel browsePageVM = mainWindow.browsePage.DataContext as BrowsePageViewModel;

            PrivateObject privBrowsePage = new PrivateObject(browsePage);
            Frame frame = (Frame)privBrowsePage.GetFieldOrProperty("mediaTableFrame");

            Type typePage = typeof(BrowseBooksPage);
            frame.NavigationService.Navigating += (sender, args) =>
            {
                Assert.AreEqual(args.Content.GetType(), typePage);
            };

            PrivateObject privBrowsePageVM = new PrivateObject(browsePageVM);
            typePage = typeof(BrowseAlbumsPage);
            privBrowsePageVM.Invoke("SelectMediaType", MediaType.Albums);
            typePage = typeof(BrowseMoviesPage);
            privBrowsePageVM.Invoke("SelectMediaType", MediaType.Movies);
            typePage = typeof(BrowseBooksPage);
            privBrowsePageVM.Invoke("SelectMediaType", MediaType.Books);
        }

        [TestMethod()]
        public void BuyBooksTest()
        {
            //Arrange
            IDisplayDialog fakeDisplayDialog = new FakeDisplayDialog();
            MainWindow mainWindow = new MainWindow(fakeDisplayDialog);
            MainWindowViewModel mainWindowVM = mainWindow.DataContext as MainWindowViewModel;
            BrowsePageViewModel browsePageVM = mainWindow.browsePage.DataContext as BrowsePageViewModel;
            BrowseBooksPage browseBooksPage = browsePageVM.browseBooksPage;
            BrowseBooksPageViewModel browseBooksPageVM = browseBooksPage.DataContext as BrowseBooksPageViewModel;
            LogInPage logInPage = mainWindow.loginPage;
            LogInPageViewModel loginPageVM = logInPage.DataContext as LogInPageViewModel;
            BookComparer bookComparer = new BookComparer();

            //Create private objects
            PrivateObject privBrowseBooksPageVM = new PrivateObject(browseBooksPageVM);
            PrivateObject privBrowsePageVM = new PrivateObject(browsePageVM);
            PrivateObject privMainWindowVM = new PrivateObject(mainWindowVM);
            PrivateObject privLoginPageVM = new PrivateObject(loginPageVM);

            //Log in
            PasswordBox passwordBox = new PasswordBox();
            loginPageVM.Username = "peyton";
            passwordBox.Password = "peydey";
            privLoginPageVM.Invoke("LogIn", passwordBox);
            User user = (User)privMainWindowVM.GetProperty("CurrentUser");

            //Act
            privBrowseBooksPageVM.Invoke("ReloadBooks"); //Selects first book 
            privBrowsePageVM.SetField("mediaType", MediaType.Books);

            //Purchase all the books
            while (browseBooksPageVM.Books.Count > Types.EMPTY_STOCK)
            {
                int countBrowseBooks = browseBooksPageVM.Books.Count;
                int countLibraryBooks = mainWindowVM.MediaLibrary.Books.Count;
                int countUserPurchasedBooks = user.Purchases.Books.Count;

                browseBooksPageVM.SelectedBook = browseBooksPageVM.Books.First(); //Select first book in browse
                int browseStock = browseBooksPageVM.SelectedBook.Stock;

                Book purchasedBook = user.Purchases.Books.Any(browseBooksPageVM.SelectedBook, bookComparer) ?
                    user.Purchases.Books.Single(browseBooksPageVM.SelectedBook, bookComparer) : null;
                int userPurchasedStock = purchasedBook != null ? purchasedBook.Stock : Types.EMPTY_STOCK;

                if (browseStock > Types.EMPTY_STOCK) //Media in stock?
                    privBrowsePageVM.Invoke("Buy");

                //Assert
                if (browseStock <= 1) //No more stock?
                {
                    Assert.IsTrue(browseBooksPageVM.Books.Count == countBrowseBooks - 1);
                    Assert.IsTrue(mainWindowVM.MediaLibrary.Books.Count == countLibraryBooks - 1);
                }
                else //Has extra stock
                {
                    Book browseBook = browseBooksPageVM.Books.Single(browseBooksPageVM.SelectedBook, bookComparer);
                    Book libraryBook = mainWindowVM.MediaLibrary.Books.Single(browseBooksPageVM.SelectedBook, bookComparer);
                    
                    Assert.AreEqual(browseBook.Stock, libraryBook.Stock);
                    Assert.AreEqual(browseBook.Stock, browseStock - 1);
                    Assert.AreEqual(libraryBook.Stock, browseStock - 1);
                }

                if (userPurchasedStock <= Types.EMPTY_STOCK)
                    Assert.AreEqual(user.Purchases.Books.Count, countUserPurchasedBooks + 1);
                else
                    Assert.AreEqual(purchasedBook.Stock, userPurchasedStock + 1);
            }
        }

        [TestMethod()]
        public void BuyAlbumsTest()
        {
            //Arrange
            IDisplayDialog fakeDisplayDialog = new FakeDisplayDialog();
            MainWindow mainWindow = new MainWindow(fakeDisplayDialog);
            MainWindowViewModel mainWindowVM = mainWindow.DataContext as MainWindowViewModel;
            BrowsePageViewModel browsePageVM = mainWindow.browsePage.DataContext as BrowsePageViewModel;
            BrowseAlbumsPage browseAlbumsPage = browsePageVM.browseAlbumsPage;
            BrowseAlbumsPageViewModel browseAlbumsPageVM = browseAlbumsPage.DataContext as BrowseAlbumsPageViewModel;
            LogInPage logInPage = mainWindow.loginPage;
            LogInPageViewModel loginPageVM = logInPage.DataContext as LogInPageViewModel;
            AlbumComparer albumComparer = new AlbumComparer();

            //Create private objects
            PrivateObject privBrowseAlbumsPageVM = new PrivateObject(browseAlbumsPageVM);
            PrivateObject privBrowsePageVM = new PrivateObject(browsePageVM);
            PrivateObject privMainWindowVM = new PrivateObject(mainWindowVM);
            PrivateObject privLoginPageVM = new PrivateObject(loginPageVM);

            //Log in
            PasswordBox passwordBox = new PasswordBox();
            loginPageVM.Username = "peyton";
            passwordBox.Password = "peydey";
            privLoginPageVM.Invoke("LogIn", passwordBox);
            User user = (User)privMainWindowVM.GetProperty("CurrentUser");

            //Act
            privBrowseAlbumsPageVM.Invoke("ReloadAlbums"); //Selects first album 
            privBrowsePageVM.SetField("mediaType", MediaType.Albums);

            //Purchase all the albums
            while (browseAlbumsPageVM.Albums.Count > 0)
            {
                int countBrowseAlbums = browseAlbumsPageVM.Albums.Count;
                int countLibraryAlbums = mainWindowVM.MediaLibrary.Albums.Count;
                int countUserPurchasedAlbums = user.Purchases.Albums.Count;

                browseAlbumsPageVM.SelectedAlbum = browseAlbumsPageVM.Albums.First(); //Select first album in browse
                int browseStock = browseAlbumsPageVM.SelectedAlbum.Stock;

                Album purchasedAlbum = user.Purchases.Albums.Any(browseAlbumsPageVM.SelectedAlbum, albumComparer) ?
                    user.Purchases.Albums.Single(browseAlbumsPageVM.SelectedAlbum, albumComparer) : null;
                int userPurchasedStock = purchasedAlbum != null ? purchasedAlbum.Stock : Types.EMPTY_STOCK;

                if (browseStock > Types.EMPTY_STOCK) //Media in stock?
                    privBrowsePageVM.Invoke("Buy");

                //Assert
                if (browseStock <= 1) //No more stock?
                {
                    Assert.IsTrue(browseAlbumsPageVM.Albums.Count == countBrowseAlbums - 1);
                    Assert.IsTrue(mainWindowVM.MediaLibrary.Albums.Count == countLibraryAlbums - 1);
                }
                else //Has extra stock
                {
                    Album browseAlbum = browseAlbumsPageVM.Albums.Single(browseAlbumsPageVM.SelectedAlbum, albumComparer);
                    Album libraryAlbum = mainWindowVM.MediaLibrary.Albums.Single(browseAlbumsPageVM.SelectedAlbum, albumComparer);

                    Assert.AreEqual(browseAlbum.Stock, libraryAlbum.Stock);
                    Assert.AreEqual(browseAlbum.Stock, browseStock - 1);
                    Assert.AreEqual(libraryAlbum.Stock, browseStock - 1);
                }

                if (userPurchasedStock <= Types.EMPTY_STOCK)
                    Assert.AreEqual(user.Purchases.Albums.Count, countUserPurchasedAlbums + 1);
                else
                    Assert.AreEqual(purchasedAlbum.Stock, userPurchasedStock + 1);
            }
        }

        [TestMethod()]
        public void BuyMoviesTest()
        {
            //Arrange
            IDisplayDialog fakeDisplayDialog = new FakeDisplayDialog();
            MainWindow mainWindow = new MainWindow(fakeDisplayDialog);
            MainWindowViewModel mainWindowVM = mainWindow.DataContext as MainWindowViewModel;
            BrowsePageViewModel browsePageVM = mainWindow.browsePage.DataContext as BrowsePageViewModel;
            BrowseMoviesPage browseMoviesPage = browsePageVM.browseMoviesPage;
            BrowseMoviesPageViewModel browseMoviesPageVM = browseMoviesPage.DataContext as BrowseMoviesPageViewModel;
            LogInPage logInPage = mainWindow.loginPage;
            LogInPageViewModel loginPageVM = logInPage.DataContext as LogInPageViewModel;
            MovieComparer movieComparer = new MovieComparer();

            //Create private objects
            PrivateObject privBrowseMoviesPageVM = new PrivateObject(browseMoviesPageVM);
            PrivateObject privBrowsePageVM = new PrivateObject(browsePageVM);
            PrivateObject privMainWindowVM = new PrivateObject(mainWindowVM);
            PrivateObject privLoginPageVM = new PrivateObject(loginPageVM);

            //Log in
            PasswordBox passwordBox = new PasswordBox();
            loginPageVM.Username = "peyton";
            passwordBox.Password = "peydey";
            privLoginPageVM.Invoke("LogIn", passwordBox);
            User user = (User)privMainWindowVM.GetProperty("CurrentUser");

            //Act
            privBrowseMoviesPageVM.Invoke("ReloadMovies"); //Selects first movie 
            privBrowsePageVM.SetField("mediaType", MediaType.Movies);

            //Purchase all the movies
            while (browseMoviesPageVM.Movies.Count > 0)
            {
                int countBrowseMovies = browseMoviesPageVM.Movies.Count;
                int countLibraryMovies = mainWindowVM.MediaLibrary.Movies.Count;
                int countUserPurchasedMovies = user.Purchases.Movies.Count;

                browseMoviesPageVM.SelectedMovie = browseMoviesPageVM.Movies.First(); //Select first movie in browse
                int browseStock = browseMoviesPageVM.SelectedMovie.Stock;

                Movie purchasedMovie = user.Purchases.Movies.Any(browseMoviesPageVM.SelectedMovie, movieComparer) ?
                    user.Purchases.Movies.Single(browseMoviesPageVM.SelectedMovie, movieComparer) : null;
                int userPurchasedStock = purchasedMovie != null ? purchasedMovie.Stock : Types.EMPTY_STOCK;

                if (browseStock > Types.EMPTY_STOCK) //Media in stock?
                    privBrowsePageVM.Invoke("Buy");

                //Assert
                if (browseStock <= 1) //No more stock?
                {
                    Assert.IsTrue(browseMoviesPageVM.Movies.Count == countBrowseMovies - 1);
                    Assert.IsTrue(mainWindowVM.MediaLibrary.Movies.Count == countLibraryMovies - 1);
                }
                else //Has extra stock
                {
                    Movie browseMovie = browseMoviesPageVM.Movies.Single(browseMoviesPageVM.SelectedMovie, movieComparer);
                    Movie libraryMovie = mainWindowVM.MediaLibrary.Movies.Single(browseMoviesPageVM.SelectedMovie, movieComparer);

                    Assert.AreEqual(browseMovie.Stock, libraryMovie.Stock);
                    Assert.AreEqual(browseMovie.Stock, browseStock - 1);
                    Assert.AreEqual(libraryMovie.Stock, browseStock - 1);
                }

                if (userPurchasedStock <= Types.EMPTY_STOCK)
                    Assert.AreEqual(user.Purchases.Movies.Count, countUserPurchasedMovies + 1);
                else
                    Assert.AreEqual(purchasedMovie.Stock, userPurchasedStock + 1);
            }
        }

        [TestMethod()]
        public void RentBooksTest()
        {
            //Arrange
            IDisplayDialog fakeDisplayDialog = new FakeDisplayDialog();
            MainWindow mainWindow = new MainWindow(fakeDisplayDialog);
            MainWindowViewModel mainWindowVM = mainWindow.DataContext as MainWindowViewModel;
            BrowsePageViewModel browsePageVM = mainWindow.browsePage.DataContext as BrowsePageViewModel;
            BrowseBooksPage browseBooksPage = browsePageVM.browseBooksPage;
            BrowseBooksPageViewModel browseBooksPageVM = browseBooksPage.DataContext as BrowseBooksPageViewModel;
            LogInPage logInPage = mainWindow.loginPage;
            LogInPageViewModel loginPageVM = logInPage.DataContext as LogInPageViewModel;
            BookComparer bookComparer = new BookComparer();

            //Create private objects
            PrivateObject privBrowseBooksPageVM = new PrivateObject(browseBooksPageVM);
            PrivateObject privBrowsePageVM = new PrivateObject(browsePageVM);
            PrivateObject privMainWindowVM = new PrivateObject(mainWindowVM);
            PrivateObject privLoginPageVM = new PrivateObject(loginPageVM);

            //Log in
            PasswordBox passwordBox = new PasswordBox();
            loginPageVM.Username = "peyton";
            passwordBox.Password = "peydey";
            privLoginPageVM.Invoke("LogIn", passwordBox);
            User user = (User)privMainWindowVM.GetProperty("CurrentUser");

            //Act
            privBrowseBooksPageVM.Invoke("ReloadBooks"); //Selects first book 
            privBrowsePageVM.SetField("mediaType", MediaType.Books);

            //Rent all the books
            while (browseBooksPageVM.Books.Any(b => b.Stock > Types.EMPTY_STOCK))
            {
                int countBrowseBooks = browseBooksPageVM.Books.Count;
                int countLibraryBooks = mainWindowVM.MediaLibrary.Books.Count;
                int countUserRentedBooks = user.Rentals.Books.Count;

                browseBooksPageVM.SelectedBook = browseBooksPageVM.Books.First(b =>
                    b.Stock > Types.EMPTY_STOCK); //Select next in-stock book in browse
                int browseStock = browseBooksPageVM.SelectedBook.Stock;

                Book userRentedBook = user.Rentals.Books.Any(browseBooksPageVM.SelectedBook, bookComparer) ?
                    user.Rentals.Books.Single(browseBooksPageVM.SelectedBook, bookComparer) : null;
                int userRentedStock = userRentedBook != null ? userRentedBook.Stock : Types.EMPTY_STOCK;

                if (browseStock > Types.EMPTY_STOCK) //Media in stock?
                    privBrowsePageVM.Invoke("Rent");

                //Assert
                if (browseStock <= 1) //No more stock?
                {
                    Assert.IsTrue(browseBooksPageVM.Books.Count == countBrowseBooks); //No change
                    Assert.IsTrue(mainWindowVM.MediaLibrary.Books.Count == countLibraryBooks); //No change
                }
                else //Has extra stock
                {
                    Book browseBook = browseBooksPageVM.Books.Single(browseBooksPageVM.SelectedBook, bookComparer);
                    Book libraryBook = mainWindowVM.MediaLibrary.Books.Single(browseBooksPageVM.SelectedBook, bookComparer);

                    Assert.AreEqual(browseBook.Stock, libraryBook.Stock);
                    Assert.AreEqual(browseBook.Stock, browseStock - 1);
                    Assert.AreEqual(libraryBook.Stock, browseStock - 1);
                }

                if (userRentedStock <= Types.EMPTY_STOCK)
                    Assert.AreEqual(user.Rentals.Books.Count, countUserRentedBooks + 1);
                else
                    Assert.AreEqual(userRentedBook.Stock, userRentedStock + 1);
            }
        }
        
        [TestMethod()]
        public void RentAlbumsTest()
        {
            //Arrange
            IDisplayDialog fakeDisplayDialog = new FakeDisplayDialog();
            MainWindow mainWindow = new MainWindow(fakeDisplayDialog);
            MainWindowViewModel mainWindowVM = mainWindow.DataContext as MainWindowViewModel;
            BrowsePageViewModel browsePageVM = mainWindow.browsePage.DataContext as BrowsePageViewModel;
            BrowseAlbumsPage browseAlbumsPage = browsePageVM.browseAlbumsPage;
            BrowseAlbumsPageViewModel browseAlbumsPageVM = browseAlbumsPage.DataContext as BrowseAlbumsPageViewModel;
            LogInPage logInPage = mainWindow.loginPage;
            LogInPageViewModel loginPageVM = logInPage.DataContext as LogInPageViewModel;
            AlbumComparer albumComparer = new AlbumComparer();

            //Create private objects
            PrivateObject privBrowseAlbumsPageVM = new PrivateObject(browseAlbumsPageVM);
            PrivateObject privBrowsePageVM = new PrivateObject(browsePageVM);
            PrivateObject privMainWindowVM = new PrivateObject(mainWindowVM);
            PrivateObject privLoginPageVM = new PrivateObject(loginPageVM);

            //Log in
            PasswordBox passwordBox = new PasswordBox();
            loginPageVM.Username = "peyton";
            passwordBox.Password = "peydey";
            privLoginPageVM.Invoke("LogIn", passwordBox);
            User user = (User)privMainWindowVM.GetProperty("CurrentUser");

            //Act
            privBrowseAlbumsPageVM.Invoke("ReloadAlbums"); //Selects first album 
            privBrowsePageVM.SetField("mediaType", MediaType.Albums);

            //Rent all the albums
            while (browseAlbumsPageVM.Albums.Any(b => b.Stock > Types.EMPTY_STOCK))
            {
                int countBrowseAlbums = browseAlbumsPageVM.Albums.Count;
                int countLibraryAlbums = mainWindowVM.MediaLibrary.Albums.Count;
                int countUserRentedAlbums = user.Rentals.Albums.Count;

                browseAlbumsPageVM.SelectedAlbum = browseAlbumsPageVM.Albums.First(b =>
                    b.Stock > Types.EMPTY_STOCK); //Select next in-stock album in browse
                int browseStock = browseAlbumsPageVM.SelectedAlbum.Stock;

                Album userRentedAlbum = user.Rentals.Albums.Any(browseAlbumsPageVM.SelectedAlbum, albumComparer) ?
                    user.Rentals.Albums.Single(browseAlbumsPageVM.SelectedAlbum, albumComparer) : null;
                int userRentedStock = userRentedAlbum != null ? userRentedAlbum.Stock : Types.EMPTY_STOCK;

                if (browseStock > Types.EMPTY_STOCK) //Media in stock?
                    privBrowsePageVM.Invoke("Rent");

                //Assert
                if (browseStock <= 1) //No more stock?
                {
                    Assert.IsTrue(browseAlbumsPageVM.Albums.Count == countBrowseAlbums); //No change
                    Assert.IsTrue(mainWindowVM.MediaLibrary.Albums.Count == countLibraryAlbums); //No change
                }
                else //Has extra stock
                {
                    Album browseAlbum = browseAlbumsPageVM.Albums.Single(browseAlbumsPageVM.SelectedAlbum, albumComparer);
                    Album libraryAlbum = mainWindowVM.MediaLibrary.Albums.Single(browseAlbumsPageVM.SelectedAlbum, albumComparer);

                    Assert.AreEqual(browseAlbum.Stock, libraryAlbum.Stock);
                    Assert.AreEqual(browseAlbum.Stock, browseStock - 1);
                    Assert.AreEqual(libraryAlbum.Stock, browseStock - 1);
                }

                if (userRentedStock <= Types.EMPTY_STOCK)
                    Assert.AreEqual(user.Rentals.Albums.Count, countUserRentedAlbums + 1);
                else
                    Assert.AreEqual(userRentedAlbum.Stock, userRentedStock + 1);
            }
        }

        [TestMethod()]
        public void RentMoviesTest()
        {
            //Arrange
            IDisplayDialog fakeDisplayDialog = new FakeDisplayDialog();
            MainWindow mainWindow = new MainWindow(fakeDisplayDialog);
            MainWindowViewModel mainWindowVM = mainWindow.DataContext as MainWindowViewModel;
            BrowsePageViewModel browsePageVM = mainWindow.browsePage.DataContext as BrowsePageViewModel;
            BrowseMoviesPage browseMoviesPage = browsePageVM.browseMoviesPage;
            BrowseMoviesPageViewModel browseMoviesPageVM = browseMoviesPage.DataContext as BrowseMoviesPageViewModel;
            LogInPage logInPage = mainWindow.loginPage;
            LogInPageViewModel loginPageVM = logInPage.DataContext as LogInPageViewModel;
            MovieComparer movieComparer = new MovieComparer();

            //Create private objects
            PrivateObject privBrowseMoviesPageVM = new PrivateObject(browseMoviesPageVM);
            PrivateObject privBrowsePageVM = new PrivateObject(browsePageVM);
            PrivateObject privMainWindowVM = new PrivateObject(mainWindowVM);
            PrivateObject privLoginPageVM = new PrivateObject(loginPageVM);

            //Log in
            PasswordBox passwordBox = new PasswordBox();
            loginPageVM.Username = "peyton";
            passwordBox.Password = "peydey";
            privLoginPageVM.Invoke("LogIn", passwordBox);
            User user = (User)privMainWindowVM.GetProperty("CurrentUser");

            //Act
            privBrowseMoviesPageVM.Invoke("ReloadMovies"); //Selects first movie 
            privBrowsePageVM.SetField("mediaType", MediaType.Movies);

            //Rent all the movies
            while (browseMoviesPageVM.Movies.Any(b => b.Stock > Types.EMPTY_STOCK))
            {
                int countBrowseMovies = browseMoviesPageVM.Movies.Count;
                int countLibraryMovies = mainWindowVM.MediaLibrary.Movies.Count;
                int countUserRentedMovies = user.Rentals.Movies.Count;

                browseMoviesPageVM.SelectedMovie = browseMoviesPageVM.Movies.First(b =>
                    b.Stock > Types.EMPTY_STOCK); //Select next in-stock movie in browse
                int browseStock = browseMoviesPageVM.SelectedMovie.Stock;

                Movie userRentedMovie = user.Rentals.Movies.Any(browseMoviesPageVM.SelectedMovie, movieComparer) ?
                    user.Rentals.Movies.Single(browseMoviesPageVM.SelectedMovie, movieComparer) : null;
                int userRentedStock = userRentedMovie != null ? userRentedMovie.Stock : Types.EMPTY_STOCK;

                if (browseStock > Types.EMPTY_STOCK) //Media in stock?
                    privBrowsePageVM.Invoke("Rent");

                //Assert
                if (browseStock <= 1) //No more stock?
                {
                    Assert.IsTrue(browseMoviesPageVM.Movies.Count == countBrowseMovies); //No change
                    Assert.IsTrue(mainWindowVM.MediaLibrary.Movies.Count == countLibraryMovies); //No change
                }
                else //Has extra stock
                {
                    Movie browseMovie = browseMoviesPageVM.Movies.Single(browseMoviesPageVM.SelectedMovie, movieComparer);
                    Movie libraryMovie = mainWindowVM.MediaLibrary.Movies.Single(browseMoviesPageVM.SelectedMovie, movieComparer);

                    Assert.AreEqual(browseMovie.Stock, libraryMovie.Stock);
                    Assert.AreEqual(browseMovie.Stock, browseStock - 1);
                    Assert.AreEqual(libraryMovie.Stock, browseStock - 1);
                }

                if (userRentedStock <= Types.EMPTY_STOCK)
                    Assert.AreEqual(user.Rentals.Movies.Count, countUserRentedMovies + 1);
                else
                    Assert.AreEqual(userRentedMovie.Stock, userRentedStock + 1);
            }
        }

        [TestMethod()]
        [DataRow(MediaType.Books, 0, false)]
        [DataRow(MediaType.Books, 1, true)]
        [DataRow(MediaType.Albums, 0, false)]
        [DataRow(MediaType.Albums, 1, true)]
        [DataRow(MediaType.Movies, 0, false)]
        [DataRow(MediaType.Movies, 1, true)]
        public void HasMadeSelectionTest(MediaType mediaType, int stock, bool shouldHaveSelection)
        {
            //Arrange
            IDisplayDialog fakeDisplayDialog = new FakeDisplayDialog();
            MainWindow mainWindow = new MainWindow(fakeDisplayDialog);
            MainWindowViewModel mainWindowVM = mainWindow.DataContext as MainWindowViewModel;
            BrowsePageViewModel browsePageVM = mainWindow.browsePage.DataContext as BrowsePageViewModel;
            BrowseBooksPage browseBooksPage = browsePageVM.browseBooksPage;
            BrowseAlbumsPage browseAlbumsPage = browsePageVM.browseAlbumsPage;
            BrowseMoviesPage browseMoviesPage = browsePageVM.browseMoviesPage;
            BrowseBooksPageViewModel browseBooksPageVM = browseBooksPage.DataContext as BrowseBooksPageViewModel;
            BrowseAlbumsPageViewModel browseAlbumsPageVM = browseAlbumsPage.DataContext as BrowseAlbumsPageViewModel;
            BrowseMoviesPageViewModel browseMoviesPageVM = browseMoviesPage.DataContext as BrowseMoviesPageViewModel;

            //Create private objects
            PrivateObject privBrowsePageVM = new PrivateObject(browsePageVM);
            privBrowsePageVM.SetField("mediaType", mediaType);

            switch (mediaType)
            {
                case MediaType.Books:
                    browseBooksPageVM.SelectedBook = new Book() { Stock = stock };
                    break;
                case MediaType.Albums:
                    browseAlbumsPageVM.SelectedAlbum = new Album() { Stock = stock };
                    break;
                case MediaType.Movies:
                    browseMoviesPageVM.SelectedMovie = new Movie() { Stock = stock };
                    break;
            }

            bool hasSelection = (bool)privBrowsePageVM.Invoke("HasMadeSelection");

            Assert.AreEqual(shouldHaveSelection, hasSelection);
        }
    }
}
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
            Assert.IsNotNull(browsePageVM.browseBooksPage);
            Assert.IsNotNull(browsePageVM.browseAlbumsPage);
            Assert.IsNotNull(browsePageVM.browseMoviesPage);
        }

        //[TestMethod()]
        //public void SelectMediaTypeTest()
        //{
        //    MainWindow mainWindow = new MainWindow();
        //    MainWindowViewModel mainWindowVM = new MainWindowViewModel(mainWindow);
        //    BrowsePageViewModel browsePageVM = new BrowsePageViewModel(mainWindowVM);
        //    BrowsePage browsePage = new BrowsePage(mainWindowVM);

        //    PrivateObject privBrowsePage = new PrivateObject(browsePage);
        //    Frame frame = (Frame)privBrowsePage.GetFieldOrProperty("mediaTableFrame");

        //    MediaType mediaTypeFound;
        //    frame.NavigationService.Navigating += (sender, args) =>
        //    {
        //        ;
        //    };

        //    PrivateObject privBrowsePageVM = new PrivateObject(browsePageVM);
        //    privBrowsePageVM.Invoke("SelectMediaType", MediaType.Books);

        //    //PrivateObject privBrowsePage = new PrivateObject(browsePage);
        //    //Frame frame = (Frame)privBrowsePage.GetFieldOrProperty("mediaTableFrame");
        //    //Assert.AreEqual(frame.Source, new Uri("BrowsePageViewModel.xaml"));
        //    Assert.IsTrue(true);
        //}

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

            while (browseBooksPageVM.Books.Count > 0)
            {
                int countBrowseBooks = browseBooksPageVM.Books.Count;
                int countLibraryBooks = mainWindowVM.MediaLibrary.Books.Count;
                int countUserPurchasedBooks = user.Purchases.Books.Count;

                browseBooksPageVM.SelectedBook = browseBooksPageVM.Books.First(); //Select first book in browse
                int browseStock = browseBooksPageVM.SelectedBook.Stock;

                Book purchasedBook = user.Purchases.Books.Any(browseBooksPageVM.SelectedBook, bookComparer) ?
                    user.Purchases.Books.Single(browseBooksPageVM.SelectedBook, bookComparer) : null;
                int userPurchasedStock = purchasedBook != null ? purchasedBook.Stock : 0;

                if (browseStock > 0) //Media in stock?
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

                if (userPurchasedStock <= 0)
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

            while (browseAlbumsPageVM.Albums.Count > 0)
            {
                int countBrowseAlbums = browseAlbumsPageVM.Albums.Count;
                int countLibraryAlbums = mainWindowVM.MediaLibrary.Albums.Count;
                int countUserPurchasedAlbums = user.Purchases.Albums.Count;

                browseAlbumsPageVM.SelectedAlbum = browseAlbumsPageVM.Albums.First(); //Select first album in browse
                int browseStock = browseAlbumsPageVM.SelectedAlbum.Stock;

                Album purchasedAlbum = user.Purchases.Albums.Any(browseAlbumsPageVM.SelectedAlbum, albumComparer) ?
                    user.Purchases.Albums.Single(browseAlbumsPageVM.SelectedAlbum, albumComparer) : null;
                int userPurchasedStock = purchasedAlbum != null ? purchasedAlbum.Stock : 0;

                if (browseStock > 0) //Media in stock?
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

                if (userPurchasedStock <= 0)
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

            while (browseMoviesPageVM.Movies.Count > 0)
            {
                int countBrowseMovies = browseMoviesPageVM.Movies.Count;
                int countLibraryMovies = mainWindowVM.MediaLibrary.Movies.Count;
                int countUserPurchasedMovies = user.Purchases.Movies.Count;

                browseMoviesPageVM.SelectedMovie = browseMoviesPageVM.Movies.First(); //Select first movie in browse
                int browseStock = browseMoviesPageVM.SelectedMovie.Stock;

                Movie purchasedMovie = user.Purchases.Movies.Any(browseMoviesPageVM.SelectedMovie, movieComparer) ?
                    user.Purchases.Movies.Single(browseMoviesPageVM.SelectedMovie, movieComparer) : null;
                int userPurchasedStock = purchasedMovie != null ? purchasedMovie.Stock : 0;

                if (browseStock > 0) //Media in stock?
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

                if (userPurchasedStock <= 0)
                    Assert.AreEqual(user.Purchases.Movies.Count, countUserPurchasedMovies + 1);
                else
                    Assert.AreEqual(purchasedMovie.Stock, userPurchasedStock + 1);
            }
        }

        //[TestMethod()]
        //public void RentTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void HasMadeSelectionTest()
        //{
        //    Assert.Fail();
        //}
    }
}
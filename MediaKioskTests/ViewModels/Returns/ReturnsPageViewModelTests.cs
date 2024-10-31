using MediaKiosk.DisplayDialogs;
using MediaKiosk.Models;
using MediaKiosk.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace MediaKiosk.ViewModels.Returns.Tests
{
    [TestClass()]
    public class ReturnsPageViewModelTests
    {
        [TestMethod()]
        public void ReturnsPageViewModelTest()
        {
            MainWindow mainWindow = new MainWindow();
            MainWindowViewModel mainWindowVM = mainWindow.DataContext as MainWindowViewModel;
            ReturnsPageViewModel returnsPageVM = mainWindow.returnsPage.DataContext as ReturnsPageViewModel;

            Assert.IsNotNull(mainWindowVM);

            PrivateObject privReturnsPageVM = new PrivateObject(returnsPageVM);
            Assert.IsNotNull(privReturnsPageVM.GetField("displayDialog"));
        }

        [TestMethod()]
        public void ReloadMediaTest()
        {
            MainWindow mainWindow = new MainWindow();
            ReturnsPageViewModel returnsPageVM = mainWindow.returnsPage.DataContext as ReturnsPageViewModel;
            LogInPage logInPage = mainWindow.loginPage;
            LogInPageViewModel loginPageVM = logInPage.DataContext as LogInPageViewModel;

            PrivateObject privLoginPageVM = new PrivateObject(loginPageVM);
            PrivateObject privReturnsPageVM = new PrivateObject(returnsPageVM);

            //Log in
            PasswordBox passwordBox = new PasswordBox();
            loginPageVM.Username = "peyton";
            passwordBox.Password = "peydey";
            privLoginPageVM.Invoke("LogIn", passwordBox);

            privReturnsPageVM.Invoke("ReloadMedia");

            Assert.IsNotNull(returnsPageVM.PurchasedMedia);
            Assert.IsNotNull(returnsPageVM.RentedMedia);
            //Assert.IsTrue(returnsPageVM.PurchasedMedia.Count > 0); //True if user has purchases
            //Assert.IsTrue(returnsPageVM.RentedMedia.Count > 0); //True if user has rentals
        }

        [TestMethod()]
        public void CompileMediaTest()
        {
            MainWindow mainWindow = new MainWindow();
            ReturnsPageViewModel returnsPageVM = mainWindow.returnsPage.DataContext as ReturnsPageViewModel;

            MediaLibrary mediaLibrary = new MediaLibrary();
            mediaLibrary.Books.Add(new Book());
            mediaLibrary.Albums.Add(new Album());
            mediaLibrary.Movies.Add(new Movie());

            PrivateObject privReturnsPageVM = new PrivateObject(returnsPageVM);
            ObservableCollection<Media> mediaCol = (ObservableCollection<Media>)privReturnsPageVM.Invoke("CompileMedia", mediaLibrary);

            Assert.IsNotNull(mediaCol);
            Assert.AreEqual(mediaCol.Count, 3);
        }

        [TestMethod()]
        [DataRow("1984", "George Orwell", "Fiction", 1949)] //Rented
        [DataRow("KJV Bible", "God", "Spiritual", 1611)] //Not rented
        public void ReturnBookTest(string title, string author, string category, int publicationYear)
        {
            //Arrange
            IDisplayDialog fakeDisplayDialog = new FakeDisplayDialog();
            MainWindow mainWindow = new MainWindow(fakeDisplayDialog);
            MainWindowViewModel mainWindowVM = mainWindow.DataContext as MainWindowViewModel;
            ReturnsPageViewModel returnsPageVM = mainWindow.returnsPage.DataContext as ReturnsPageViewModel;
            LogInPage logInPage = mainWindow.loginPage;
            LogInPageViewModel loginPageVM = logInPage.DataContext as LogInPageViewModel;
            BookComparer comparer = new BookComparer();

            //Create private objects
            PrivateObject privReturnsPageVM = new PrivateObject(returnsPageVM);
            PrivateObject privLoginPageVM = new PrivateObject(loginPageVM);

            //Log in
            PasswordBox passwordBox = new PasswordBox();
            loginPageVM.Username = "peyton";
            passwordBox.Password = "peydey";
            privLoginPageVM.Invoke("LogIn", passwordBox);

            //Simulate loading page
            privReturnsPageVM.Invoke("ReloadMedia");

            //Check copies of media in library before return
            bool hasBookInLibraryInit = mainWindowVM.MediaLibrary.Books.Any(b =>
                b.Title == title && b.Author == author && b.Category == category
                && b.PublicationYear == publicationYear);

            int stockInLibraryInit = 0;
            if (hasBookInLibraryInit)
            {
                stockInLibraryInit = mainWindowVM.MediaLibrary.Books.Single(b =>
                    b.Title == title && b.Author == author && b.Category == category
                    && b.PublicationYear == publicationYear).Stock;
            }

            //Check copies of media the user has rented
            List<Book> booksUserRented = mainWindowVM.CurrentUser.Rentals.Books;

            bool hasBookInRentalsInit = booksUserRented.Any(b =>
                b.Title == title && b.Author == author && b.Category == category
                && b.PublicationYear == publicationYear);

            int stockInRentalsInit = 0;
            if (hasBookInRentalsInit)
            {
                stockInRentalsInit = booksUserRented.Single(b =>
                    b.Title == title && b.Author == author && b.Category == category
                    && b.PublicationYear == publicationYear).Stock;
            }

            //Check displayed rented media
            IEnumerable<Book> booksRentedOnDisplayInit = returnsPageVM.RentedMedia.Where(m =>
                m.GetType() == typeof(Book)).Cast<Book>();

            bool hasBookInDisplayRentalsInit = booksRentedOnDisplayInit.Any(b =>
                b.Title == title && b.Author == author && b.Category == category
                && b.PublicationYear == publicationYear);

            int stockInDisplayRentalsInit = 0;
            if (hasBookInDisplayRentalsInit)
            {
                stockInDisplayRentalsInit = booksRentedOnDisplayInit.Single(b =>
                    b.Title == title && b.Author == author && b.Category == category
                    && b.PublicationYear == publicationYear).Stock;
            }

            //Create new media
            Book book = new Book()
            {
                Title = title,
                Author = author,
                Category = category,
                PublicationYear = publicationYear,
                Stock = 1
            };

            //Act
            privReturnsPageVM.Invoke("Return", book);

            //Check copies of media in library after return
            bool hasBookInLibraryAfter = mainWindowVM.MediaLibrary.Books.Any(b =>
                b.Title == title && b.Author == author && b.Category == category
                && b.PublicationYear == publicationYear);

            int stockInLibraryAfter = 0;
            if (hasBookInLibraryAfter)
            {
                stockInLibraryAfter = mainWindowVM.MediaLibrary.Books.Single(b =>
                    b.Title == title && b.Author == author && b.Category == category
                    && b.PublicationYear == publicationYear).Stock;
            }

            //Check copies of media the user has rented after return
            bool hasBookInRentalsAfter = booksUserRented.Any(b =>
                b.Title == title && b.Author == author && b.Category == category
                && b.PublicationYear == publicationYear);

            int stockInRentalsAfter = Types.EMPTY_STOCK;
            if (hasBookInRentalsAfter)
            {
                stockInRentalsAfter = booksUserRented.Single(b =>
                    b.Title == title && b.Author == author && b.Category == category
                    && b.PublicationYear == publicationYear).Stock;
            }

            //Check displayed rented media after return
            IEnumerable<Book> booksRentedOnDisplayAfter = returnsPageVM.RentedMedia.Where(m =>
                m.GetType() == typeof(Book)).Cast<Book>();

            bool hasBookInDisplayRentalsAfter = booksRentedOnDisplayAfter.Any(b =>
                b.Title == title && b.Author == author && b.Category == category
                && b.PublicationYear == publicationYear);

            int stockInDisplayRentalsAfter = Types.EMPTY_STOCK;
            if (hasBookInDisplayRentalsAfter)
            {
                stockInDisplayRentalsAfter = booksRentedOnDisplayAfter.Single(b =>
                    b.Title == title && b.Author == author && b.Category == category
                    && b.PublicationYear == publicationYear).Stock;
            }

            //Assert
            if (hasBookInLibraryInit)
                Assert.AreEqual(stockInLibraryInit + 1, stockInLibraryAfter);
            else
                Assert.AreEqual(stockInLibraryAfter, 1);

            if (stockInRentalsInit > Types.EMPTY_STOCK)
                Assert.AreEqual(stockInRentalsInit - 1, stockInRentalsAfter);
            else
                Assert.AreEqual(stockInRentalsAfter, Types.EMPTY_STOCK);

            if (stockInRentalsAfter <= Types.EMPTY_STOCK)
                Assert.IsFalse(booksUserRented.Contains(book, comparer));

            if (stockInDisplayRentalsInit > Types.EMPTY_STOCK)
                Assert.AreEqual(stockInDisplayRentalsInit - 1, stockInDisplayRentalsAfter);
            else
                Assert.AreEqual(stockInDisplayRentalsAfter, Types.EMPTY_STOCK);

            if (stockInDisplayRentalsAfter <= Types.EMPTY_STOCK)
                Assert.IsFalse(booksRentedOnDisplayAfter.Contains(book, comparer));
        }

        [TestMethod()]
        [DataRow("Let You Sleep", "Lastwatch", "Hard Rock", 2017)] //Rented
        [DataRow("Flyleaf", "Flyleaf", "Rock", 2005)] //Not rented
        public void ReturnAlbumTest(string title, string artist, string genre, int releaseYear)
        {
            //Arrange
            IDisplayDialog fakeDisplayDialog = new FakeDisplayDialog();
            MainWindow mainWindow = new MainWindow(fakeDisplayDialog);
            MainWindowViewModel mainWindowVM = mainWindow.DataContext as MainWindowViewModel;
            ReturnsPageViewModel returnsPageVM = mainWindow.returnsPage.DataContext as ReturnsPageViewModel;
            LogInPage logInPage = mainWindow.loginPage;
            LogInPageViewModel loginPageVM = logInPage.DataContext as LogInPageViewModel;
            AlbumComparer comparer = new AlbumComparer();

            //Create private objects
            PrivateObject privReturnsPageVM = new PrivateObject(returnsPageVM);
            PrivateObject privLoginPageVM = new PrivateObject(loginPageVM);

            //Log in
            PasswordBox passwordBox = new PasswordBox();
            loginPageVM.Username = "peyton";
            passwordBox.Password = "peydey";
            privLoginPageVM.Invoke("LogIn", passwordBox);

            //Simulate loading page
            privReturnsPageVM.Invoke("ReloadMedia");

            //Check copies of media in library before return
            bool hasAlbumInLibraryInit = mainWindowVM.MediaLibrary.Albums.Any(b =>
                b.Title == title && b.Artist == artist && b.Genre == genre
                && b.ReleaseYear == Convert.ToInt32(releaseYear));

            int stockInLibraryInit = 0;
            if (hasAlbumInLibraryInit)
            {
                stockInLibraryInit = mainWindowVM.MediaLibrary.Albums.Single(b =>
                    b.Title == title && b.Artist == artist && b.Genre == genre
                    && b.ReleaseYear == Convert.ToInt32(releaseYear)).Stock;
            }

            //Check copies of media the user has rented
            List<Album> albumsUserRented = mainWindowVM.CurrentUser.Rentals.Albums;

            bool hasAlbumInRentalsInit = albumsUserRented.Any(b =>
                b.Title == title && b.Artist == artist && b.Genre == genre
                && b.ReleaseYear == Convert.ToInt32(releaseYear));

            int stockInRentalsInit = 0;
            if (hasAlbumInRentalsInit)
            {
                stockInRentalsInit = albumsUserRented.Single(b =>
                    b.Title == title && b.Artist == artist && b.Genre == genre
                    && b.ReleaseYear == Convert.ToInt32(releaseYear)).Stock;
            }

            //Check displayed rented media
            IEnumerable<Album> albumsRentedOnDisplayInit = returnsPageVM.RentedMedia.Where(m =>
                m.GetType() == typeof(Album)).Cast<Album>();

            bool hasAlbumInDisplayRentalsInit = albumsRentedOnDisplayInit.Any(b =>
                b.Title == title && b.Artist == artist && b.Genre == genre
                && b.ReleaseYear == Convert.ToInt32(releaseYear));

            int stockInDisplayRentalsInit = 0;
            if (hasAlbumInDisplayRentalsInit)
            {
                stockInDisplayRentalsInit = albumsRentedOnDisplayInit.Single(b =>
                    b.Title == title && b.Artist == artist && b.Genre == genre
                    && b.ReleaseYear == Convert.ToInt32(releaseYear)).Stock;
            }

            //Create new media
            Album album = new Album()
            {
                Title = title,
                Artist = artist,
                Genre = genre,
                ReleaseYear = releaseYear,
                Stock = 1
            };

            //Act
            privReturnsPageVM.Invoke("Return", album);

            //Check copies of media in library after return
            bool hasAlbumInLibraryAfter = mainWindowVM.MediaLibrary.Albums.Any(b =>
                b.Title == title && b.Artist == artist && b.Genre == genre
                && b.ReleaseYear == Convert.ToInt32(releaseYear));

            int stockInLibraryAfter = 0;
            if (hasAlbumInLibraryAfter)
            {
                stockInLibraryAfter = mainWindowVM.MediaLibrary.Albums.Single(b =>
                    b.Title == title && b.Artist == artist && b.Genre == genre
                    && b.ReleaseYear == Convert.ToInt32(releaseYear)).Stock;
            }

            //Check copies of media the user has rented after return
            bool hasAlbumInRentalsAfter = albumsUserRented.Any(b =>
                b.Title == title && b.Artist == artist && b.Genre == genre
                && b.ReleaseYear == Convert.ToInt32(releaseYear));

            int stockInRentalsAfter = Types.EMPTY_STOCK;
            if (hasAlbumInRentalsAfter)
            {
                stockInRentalsAfter = albumsUserRented.Single(b =>
                    b.Title == title && b.Artist == artist && b.Genre == genre
                    && b.ReleaseYear == Convert.ToInt32(releaseYear)).Stock;
            }

            //Check displayed rented media after return
            IEnumerable<Album> albumsRentedOnDisplayAfter = returnsPageVM.RentedMedia.Where(m =>
                m.GetType() == typeof(Album)).Cast<Album>();

            bool hasAlbumInDisplayRentalsAfter = albumsRentedOnDisplayAfter.Any(b =>
                b.Title == title && b.Artist == artist && b.Genre == genre
                && b.ReleaseYear == Convert.ToInt32(releaseYear));

            int stockInDisplayRentalsAfter = Types.EMPTY_STOCK;
            if (hasAlbumInDisplayRentalsAfter)
            {
                stockInDisplayRentalsAfter = albumsRentedOnDisplayAfter.Single(b =>
                    b.Title == title && b.Artist == artist && b.Genre == genre
                    && b.ReleaseYear == Convert.ToInt32(releaseYear)).Stock;
            }

            //Assert
            if (hasAlbumInLibraryInit)
                Assert.AreEqual(stockInLibraryInit + 1, stockInLibraryAfter);
            else
                Assert.AreEqual(stockInLibraryAfter, 1);

            if (stockInRentalsInit > Types.EMPTY_STOCK)
                Assert.AreEqual(stockInRentalsInit - 1, stockInRentalsAfter);
            else
                Assert.AreEqual(stockInRentalsAfter, Types.EMPTY_STOCK);

            if (stockInRentalsAfter <= Types.EMPTY_STOCK)
                Assert.IsFalse(albumsUserRented.Contains(album, comparer));

            if (stockInDisplayRentalsInit > Types.EMPTY_STOCK)
                Assert.AreEqual(stockInDisplayRentalsInit - 1, stockInDisplayRentalsAfter);
            else
                Assert.AreEqual(stockInDisplayRentalsAfter, Types.EMPTY_STOCK);

            if (stockInDisplayRentalsAfter <= Types.EMPTY_STOCK)
                Assert.IsFalse(albumsRentedOnDisplayAfter.Contains(album, comparer));
        }

        [TestMethod()]
        [DataRow("National Treasure", "☆☆☆☆", "Mystery", 2006)] //Rented
        [DataRow("The Hobbit", "☆☆☆☆", "Adventure", 2012)] //Not rented
        public void ReturnMovieTest(string title, string rating, string category, int releaseYear)
        {
            //Arrange
            IDisplayDialog fakeDisplayDialog = new FakeDisplayDialog();
            MainWindow mainWindow = new MainWindow(fakeDisplayDialog);
            MainWindowViewModel mainWindowVM = mainWindow.DataContext as MainWindowViewModel;
            ReturnsPageViewModel returnsPageVM = mainWindow.returnsPage.DataContext as ReturnsPageViewModel;
            LogInPage logInPage = mainWindow.loginPage;
            LogInPageViewModel loginPageVM = logInPage.DataContext as LogInPageViewModel;
            MovieComparer comparer = new MovieComparer();

            //Create private objects
            PrivateObject privReturnsPageVM = new PrivateObject(returnsPageVM);
            PrivateObject privLoginPageVM = new PrivateObject(loginPageVM);

            //Log in
            PasswordBox passwordBox = new PasswordBox();
            loginPageVM.Username = "peyton";
            passwordBox.Password = "peydey";
            privLoginPageVM.Invoke("LogIn", passwordBox);

            //Simulate loading page
            privReturnsPageVM.Invoke("ReloadMedia");

            //Check copies of media in library before return
            bool hasMovieInLibraryInit = mainWindowVM.MediaLibrary.Movies.Any(b =>
                b.Title == title && b.Rating == rating && b.Category == category
                && b.ReleaseYear == releaseYear);

            int stockInLibraryInit = 0;
            if (hasMovieInLibraryInit)
            {
                stockInLibraryInit = mainWindowVM.MediaLibrary.Movies.Single(b =>
                    b.Title == title && b.Rating == rating && b.Category == category
                    && b.ReleaseYear == releaseYear).Stock;
            }

            //Check copies of media the user has rented
            List<Movie> moviesUserRented = mainWindowVM.CurrentUser.Rentals.Movies;

            bool hasMovieInRentalsInit = moviesUserRented.Any(b =>
                b.Title == title && b.Rating == rating && b.Category == category
                && b.ReleaseYear == releaseYear);

            int stockInRentalsInit = 0;
            if (hasMovieInRentalsInit)
            {
                stockInRentalsInit = moviesUserRented.Single(b =>
                    b.Title == title && b.Rating == rating && b.Category == category
                    && b.ReleaseYear == releaseYear).Stock;
            }

            //Check displayed rented media
            IEnumerable<Movie> moviesRentedOnDisplayInit = returnsPageVM.RentedMedia.Where(m =>
                m.GetType() == typeof(Movie)).Cast<Movie>();

            bool hasMovieInDisplayRentalsInit = moviesRentedOnDisplayInit.Any(b =>
                b.Title == title && b.Rating == rating && b.Category == category
                && b.ReleaseYear == releaseYear);

            int stockInDisplayRentalsInit = 0;
            if (hasMovieInDisplayRentalsInit)
            {
                stockInDisplayRentalsInit = moviesRentedOnDisplayInit.Single(b =>
                    b.Title == title && b.Rating == rating && b.Category == category
                    && b.ReleaseYear == releaseYear).Stock;
            }

            //Create new media
            Movie movie = new Movie()
            {
                Title = title,
                Rating = rating,
                Category = category,
                ReleaseYear = releaseYear,
                Stock = 1
            };

            //Act
            privReturnsPageVM.Invoke("Return", movie);

            //Check copies of media in library after return
            bool hasMovieInLibraryAfter = mainWindowVM.MediaLibrary.Movies.Any(b =>
                b.Title == title && b.Rating == rating && b.Category == category
                && b.ReleaseYear == releaseYear);

            int stockInLibraryAfter = 0;
            if (hasMovieInLibraryAfter)
            {
                stockInLibraryAfter = mainWindowVM.MediaLibrary.Movies.Single(b =>
                    b.Title == title && b.Rating == rating && b.Category == category
                    && b.ReleaseYear == releaseYear).Stock;
            }

            //Check copies of media the user has rented after return
            bool hasMovieInRentalsAfter = moviesUserRented.Any(b =>
                b.Title == title && b.Rating == rating && b.Category == category
                && b.ReleaseYear == releaseYear);

            int stockInRentalsAfter = Types.EMPTY_STOCK;
            if (hasMovieInRentalsAfter)
            {
                stockInRentalsAfter = moviesUserRented.Single(b =>
                    b.Title == title && b.Rating == rating && b.Category == category
                    && b.ReleaseYear == releaseYear).Stock;
            }

            //Check displayed rented media after return
            IEnumerable<Movie> moviesRentedOnDisplayAfter = returnsPageVM.RentedMedia.Where(m =>
                m.GetType() == typeof(Movie)).Cast<Movie>();

            bool hasMovieInDisplayRentalsAfter = moviesRentedOnDisplayAfter.Any(b =>
                b.Title == title && b.Rating == rating && b.Category == category
                && b.ReleaseYear == releaseYear);

            int stockInDisplayRentalsAfter = Types.EMPTY_STOCK;
            if (hasMovieInDisplayRentalsAfter)
            {
                stockInDisplayRentalsAfter = moviesRentedOnDisplayAfter.Single(b =>
                    b.Title == title && b.Rating == rating && b.Category == category
                    && b.ReleaseYear == releaseYear).Stock;
            }

            //Assert
            if (hasMovieInLibraryInit)
                Assert.AreEqual(stockInLibraryInit + 1, stockInLibraryAfter);
            else
                Assert.AreEqual(stockInLibraryAfter, 1);

            if (stockInRentalsInit > Types.EMPTY_STOCK)
                Assert.AreEqual(stockInRentalsInit - 1, stockInRentalsAfter);
            else
                Assert.AreEqual(stockInRentalsAfter, Types.EMPTY_STOCK);

            if (stockInRentalsAfter <= Types.EMPTY_STOCK)
                Assert.IsFalse(moviesUserRented.Contains(movie, comparer));

            if (stockInDisplayRentalsInit > Types.EMPTY_STOCK)
                Assert.AreEqual(stockInDisplayRentalsInit - 1, stockInDisplayRentalsAfter);
            else
                Assert.AreEqual(stockInDisplayRentalsAfter, Types.EMPTY_STOCK);

            if (stockInDisplayRentalsAfter <= Types.EMPTY_STOCK)
                Assert.IsFalse(moviesRentedOnDisplayAfter.Contains(movie, comparer));
        }
    }
}
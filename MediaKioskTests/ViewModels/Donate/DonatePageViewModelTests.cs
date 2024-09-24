using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaKiosk.ViewModels.Donate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaKiosk.ViewModels.Browse;
using MediaKiosk.Views;
using MediaKiosk.Models;
using MediaKiosk.DisplayDialogs;
using MediaKiosk.Views.Browse;
using System.Windows.Controls;
using MediaKiosk.Views.Donate;

namespace MediaKiosk.ViewModels.Donate.Tests
{
    [TestClass()]
    public class DonatePageViewModelTests
    {
        [TestMethod()]
        public void DonatePageViewModelTest()
        {
            MainWindow mainWindow = new MainWindow();
            MainWindowViewModel mainWindowVM = mainWindow.DataContext as MainWindowViewModel;
            DonatePageViewModel donatePageVM = mainWindow.donatePage.DataContext as DonatePageViewModel;

            Assert.IsNotNull(mainWindowVM);
            Assert.IsNotNull(donatePageVM);
            Assert.IsNotNull(donatePageVM.bookDonationPage);
            Assert.IsNotNull(donatePageVM.albumDonationPage);
            Assert.IsNotNull(donatePageVM.movieDonationPage);
            Assert.IsNotNull(donatePageVM.bookDonationPage.DataContext as BookDonationPageViewModel);
            Assert.IsNotNull(donatePageVM.albumDonationPage.DataContext as AlbumDonationPageViewModel);
            Assert.IsNotNull(donatePageVM.movieDonationPage.DataContext as MovieDonationPageViewModel);

            PrivateObject privDonatePageVM = new PrivateObject(donatePageVM);
            Assert.IsNotNull(privDonatePageVM.GetField("displayDialog"));
        }

        [TestMethod()]
        public void SelectMediaTypeTest()
        {
            IDisplayDialog fakeDisplayDialog = new FakeDisplayDialog();
            MainWindow mainWindow = new MainWindow(fakeDisplayDialog);
            MainWindowViewModel mainWindowVM = mainWindow.DataContext as MainWindowViewModel;
            DonatePage donatePage = mainWindow.donatePage;
            DonatePageViewModel donatePageVM = mainWindow.donatePage.DataContext as DonatePageViewModel;

            PrivateObject privDonatePage = new PrivateObject(donatePage);
            Frame frame = (Frame)privDonatePage.GetFieldOrProperty("mediaTableFrame");

            Type typePage = typeof(BookDonationPage);
            frame.NavigationService.Navigating += (sender, args) =>
            {
                Assert.AreEqual(args.Content.GetType(), typePage);
            };

            PrivateObject privDonatePageVM = new PrivateObject(donatePageVM);
            typePage = typeof(AlbumDonationPage);
            privDonatePageVM.Invoke("SelectMediaType", MediaType.Albums);
            typePage = typeof(MovieDonationPage);
            privDonatePageVM.Invoke("SelectMediaType", MediaType.Movies);
            typePage = typeof(BookDonationPage);
            privDonatePageVM.Invoke("SelectMediaType", MediaType.Books);
        }

        [TestMethod()]
        [DataRow("NIV Bible", "God", "Spiritual", "1973", @".\Resources\sample.png", false)]
        [DataRow("KJV Bible", "God", "Spiritual", "1611", @".\Resources\sample.png", true)]
        public void DonateBookTest(string title, string author, string category,
            string publicationYear, string coverArtFilePath, bool shouldHaveInStock)
        {
            //Arrange
            IDisplayDialog fakeDisplayDialog = new FakeDisplayDialog();
            MainWindow mainWindow = new MainWindow(fakeDisplayDialog);
            MainWindowViewModel mainWindowVM = mainWindow.DataContext as MainWindowViewModel;
            DonatePageViewModel donatePageVM = mainWindow.donatePage.DataContext as DonatePageViewModel;
            BookDonationPageViewModel bookDonationPageVM = donatePageVM.bookDonationPage.DataContext as BookDonationPageViewModel;

            //Create private objects
            PrivateObject privMainWindowVM = new PrivateObject(mainWindowVM);
            PrivateObject privDonatePageVM = new PrivateObject(donatePageVM);

            //Set media details
            bookDonationPageVM.Title = title;
            bookDonationPageVM.Author = author;
            bookDonationPageVM.Category = category;
            bookDonationPageVM.PublicationYear = publicationYear;
            bookDonationPageVM.CoverArtFilePath = coverArtFilePath;

            //Check copies of media before donation
            bool hasBookInit = donatePageVM.MediaLibrary.Books.Any(b =>
                b.Title == title && b.Author == author && b.Category == category
                && b.PublicationYear == Convert.ToInt32(publicationYear));

            int stockInit = 0;
            if (hasBookInit)
            {
                stockInit = donatePageVM.MediaLibrary.Books.Single(b =>
                    b.Title == title && b.Author == author && b.Category == category
                    && b.PublicationYear == Convert.ToInt32(publicationYear)).Stock;
            }

            //Act
            privDonatePageVM.SetField("mediaType", MediaType.Books);
            privDonatePageVM.Invoke("Donate");

            //Check copies of media after donation
            bool hasBookAfterDonation = donatePageVM.MediaLibrary.Books.Any(b =>
                b.Title == title && b.Author == author && b.Category == category
                && b.PublicationYear == Convert.ToInt32(publicationYear));

            int stockAfter = 1;
            if (hasBookAfterDonation)
            {
                stockAfter = donatePageVM.MediaLibrary.Books.Single(b =>
                    b.Title == title && b.Author == author && b.Category == category
                    && b.PublicationYear == Convert.ToInt32(publicationYear)).Stock;
            }

            //Assert
            if (shouldHaveInStock)
                Assert.IsTrue(hasBookInit);

            if (hasBookInit)
                Assert.AreEqual(stockInit + 1, stockAfter);
            else
                Assert.AreEqual(stockAfter, 1);
        }

        [TestMethod()]
        [DataRow("Even Worse", "Weird Al", "Comedy", "1988", @".\Resources\sample.png", false)]
        [DataRow("Thriller", "Micheal Jackson", "Pop", "1982", @".\Resources\sample.png", true)]
        public void DonateAlbumTest(string title, string artist, string genre,
            string releaseYear, string albumArtFilePath, bool shouldHaveInStock)
        {
            //Arrange
            IDisplayDialog fakeDisplayDialog = new FakeDisplayDialog();
            MainWindow mainWindow = new MainWindow(fakeDisplayDialog);
            MainWindowViewModel mainWindowVM = mainWindow.DataContext as MainWindowViewModel;
            DonatePageViewModel donatePageVM = mainWindow.donatePage.DataContext as DonatePageViewModel;
            AlbumDonationPageViewModel albumDonationPageVM = donatePageVM.albumDonationPage.DataContext as AlbumDonationPageViewModel;

            //Create private objects
            PrivateObject privMainWindowVM = new PrivateObject(mainWindowVM);
            PrivateObject privDonatePageVM = new PrivateObject(donatePageVM);

            //Set media details
            albumDonationPageVM.Title = title;
            albumDonationPageVM.Artist = artist;
            albumDonationPageVM.Genre = genre;
            albumDonationPageVM.ReleaseYear = releaseYear;
            albumDonationPageVM.AlbumArtFilePath = albumArtFilePath;

            //Check copies of media before donation
            bool hasAlbumInit = donatePageVM.MediaLibrary.Albums.Any(b =>
                b.Title == title && b.Artist == artist && b.Genre == genre
                && b.ReleaseYear == Convert.ToInt32(releaseYear));

            int stockInit = 0;
            if (hasAlbumInit)
            {
                stockInit = donatePageVM.MediaLibrary.Albums.Single(b =>
                    b.Title == title && b.Artist == artist && b.Genre == genre
                    && b.ReleaseYear == Convert.ToInt32(releaseYear)).Stock;
            }

            //Act
            privDonatePageVM.SetField("mediaType", MediaType.Albums);
            privDonatePageVM.Invoke("Donate");

            //Check copies of media after donation
            bool hasAlbumAfterDonation = donatePageVM.MediaLibrary.Albums.Any(b =>
                b.Title == title && b.Artist == artist && b.Genre == genre
                && b.ReleaseYear == Convert.ToInt32(releaseYear));

            int stockAfter = 1;
            if (hasAlbumAfterDonation)
            {
                stockAfter = donatePageVM.MediaLibrary.Albums.Single(b =>
                    b.Title == title && b.Artist == artist && b.Genre == genre
                    && b.ReleaseYear == Convert.ToInt32(releaseYear)).Stock;
            }

            //Assert
            if (shouldHaveInStock)
                Assert.IsTrue(hasAlbumInit);

            if (hasAlbumInit)
                Assert.AreEqual(stockInit + 1, stockAfter);
            else
                Assert.AreEqual(stockAfter, 1);
        }

        [TestMethod()]
        [DataRow("Despicable Me 4", "☆☆☆", "Comedy", "2024", @".\Resources\sample.png", false)]
        [DataRow("The Hobbit", "☆☆☆☆", "Adventure", "2012", @".\Resources\sample.png", true)]
        public void DonateMovieTest(string title, string rating, string category,
            string releaseYear, string promoArtFilePath, bool shouldHaveInStock)
        {
            //Arrange
            IDisplayDialog fakeDisplayDialog = new FakeDisplayDialog();
            MainWindow mainWindow = new MainWindow(fakeDisplayDialog);
            MainWindowViewModel mainWindowVM = mainWindow.DataContext as MainWindowViewModel;
            DonatePageViewModel donatePageVM = mainWindow.donatePage.DataContext as DonatePageViewModel;
            MovieDonationPageViewModel movieDonationPageVM = donatePageVM.movieDonationPage.DataContext as MovieDonationPageViewModel;

            //Create private objects
            PrivateObject privMainWindowVM = new PrivateObject(mainWindowVM);
            PrivateObject privDonatePageVM = new PrivateObject(donatePageVM);

            //Set media details
            movieDonationPageVM.Title = title;
            movieDonationPageVM.Rating = rating;
            movieDonationPageVM.Category = category;
            movieDonationPageVM.ReleaseYear = releaseYear;
            movieDonationPageVM.PromoArtFilePath = promoArtFilePath;

            //Check copies of media before donation
            bool hasMovieInit = donatePageVM.MediaLibrary.Movies.Any(b =>
                b.Title == title && b.Rating == rating && b.Category == category
                && b.ReleaseYear == Convert.ToInt32(releaseYear));

            int stockInit = 0;
            if (hasMovieInit)
            {
                stockInit = donatePageVM.MediaLibrary.Movies.Single(b =>
                    b.Title == title && b.Rating == rating && b.Category == category
                    && b.ReleaseYear == Convert.ToInt32(releaseYear)).Stock;
            }

            //Act
            privDonatePageVM.SetField("mediaType", MediaType.Movies);
            privDonatePageVM.Invoke("Donate");

            //Check copies of media after donation
            bool hasMovieAfterDonation = donatePageVM.MediaLibrary.Movies.Any(b =>
                b.Title == title && b.Rating == rating && b.Category == category
                && b.ReleaseYear == Convert.ToInt32(releaseYear));

            int stockAfter = 1;
            if (hasMovieAfterDonation)
            {
                stockAfter = donatePageVM.MediaLibrary.Movies.Single(b =>
                    b.Title == title && b.Rating == rating && b.Category == category
                    && b.ReleaseYear == Convert.ToInt32(releaseYear)).Stock;
            }

            //Assert
            if (shouldHaveInStock)
                Assert.IsTrue(hasMovieInit);

            if (hasMovieInit)
                Assert.AreEqual(stockInit + 1, stockAfter);
            else
                Assert.AreEqual(stockAfter, 1);
        }

        [TestMethod()]
        [DataRow(MediaType.Books)]
        [DataRow(MediaType.Albums)]
        [DataRow(MediaType.Movies)]
        public void IsMediaAcceptableTest(MediaType mediaType)
        {
            MainWindow mainWindow = new MainWindow();
            DonatePageViewModel donatePageVM = mainWindow.donatePage.DataContext as DonatePageViewModel;
            
            PrivateObject privDonatePageVM = new PrivateObject(donatePageVM);
            privDonatePageVM.SetField("mediaType", mediaType);
            object result = privDonatePageVM.Invoke("IsMediaAcceptable");

            Assert.IsNotNull(result); //Should be bool
        }

        [TestMethod()]
        public void GetRandomPriceTest()
        {
            string priceString = DonatePageViewModel.GetRandomPrice();

            Assert.IsNotNull(priceString);
            decimal price = Convert.ToDecimal(priceString.Substring(1)); //'$' is one char

            PrivateType privDonatePAgeVM = new PrivateType(typeof(DonatePageViewModel));
            decimal minPrice = (decimal)privDonatePAgeVM.GetStaticField("MIN_PRICE");
            decimal maxPrice = (decimal)privDonatePAgeVM.GetStaticField("MAX_PRICE");

            Assert.IsTrue(price >= minPrice && price < maxPrice);
        }
    }
}
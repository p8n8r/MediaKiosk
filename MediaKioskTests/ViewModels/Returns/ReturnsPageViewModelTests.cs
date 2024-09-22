using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaKiosk.ViewModels.Returns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaKiosk.ViewModels.Donate;
using MediaKiosk.Views;
using MediaKiosk.Views.Donate;
using MediaKiosk.Models;
using System.Collections.ObjectModel;
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
        //[DataRow("NIV Bible", "God", "Spiritual", "1973", @".\Resources\sample.png", false)]
        [DataRow("KJV Bible", "God", "Spiritual", "1611", 1)]
        public void ReturnBookTest(string title, string author, string category,
            string publicationYear, int stock)
        {
            //Arrange
            MainWindow mainWindow = new MainWindow();
            MainWindowViewModel mainWindowVM = mainWindow.DataContext as MainWindowViewModel;
            ReturnsPageViewModel returnsPageVM = mainWindow.returnsPage.DataContext as ReturnsPageViewModel;
            PrivateObject privReturnsPageVM = new PrivateObject(returnsPageVM);

            ////Check copies of media before return
            //bool hasBookInit = mainWindowVM.MediaLibrary.Books.Any(b =>
            //    b.Title == title && b.Author == author && b.Category == category
            //    && b.PublicationYear == Convert.ToInt32(publicationYear));

            //int stockInit = 0;
            //if (hasBookInit)
            //{
            //    stockInit = mainWindowVM.MediaLibrary.Books.Single(b =>
            //        b.Title == title && b.Author == author && b.Category == category
            //        && b.PublicationYear == Convert.ToInt32(publicationYear)).Stock;
            //}

            List<Book> booksReturnedInit = mainWindowVM.CurrentUser.Rentals.Books.ToList();

            //Create new media
            Book book = new Book()
            {
                Title = title,
                Author = author,
                Category = category,
                PublicationYear = Convert.ToInt32(publicationYear),
                Stock = stock
            };

            //Act
            privReturnsPageVM.Invoke("Return", book);

            ////Check copies of media after return
            //bool hasBookAfterDonation = mainWindowVM.MediaLibrary.Books.Any(b =>
            //    b.Title == title && b.Author == author && b.Category == category
            //    && b.PublicationYear == Convert.ToInt32(publicationYear));

            //int stockAfter = 1;
            //if (hasBookAfterDonation)
            //{
            //    stockAfter = mainWindowVM.MediaLibrary.Books.Single(b =>
            //        b.Title == title && b.Author == author && b.Category == category
            //        && b.PublicationYear == Convert.ToInt32(publicationYear)).Stock;
            //}

            List<Book> booksReturnedAfter = mainWindowVM.CurrentUser.Rentals.Books.ToList();


        }
    }
}
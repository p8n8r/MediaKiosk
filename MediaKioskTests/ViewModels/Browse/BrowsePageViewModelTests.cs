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
            BookComparer bookComparer = new BookComparer();

            PrivateObject privBrowseBooksPageVM = new PrivateObject(browseBooksPageVM);
            PrivateObject privBrowsePageVM = new PrivateObject(browsePageVM);

            User user = new User();
            mainWindowVM.CurrentUser = user;

            privBrowseBooksPageVM.Invoke("ReloadBooks"); //Selects first book 
            privBrowsePageVM.SetField("mediaType", MediaType.Books);

            //Act
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
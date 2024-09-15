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

namespace MediaKiosk.ViewModels.Browse.Tests
{
    [TestClass()]
    public class BrowsePageViewModelTests
    {
        [TestMethod()]
        public void BrowsePageViewModelTest()
        {
            MainWindow mainWindow = new MainWindow();
            MainWindowViewModel mainWindowVM = new MainWindowViewModel(mainWindow);
            BrowsePageViewModel browsePageVM = new BrowsePageViewModel(mainWindowVM);

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

        //[TestMethod()]
        //public void BuyTest()
        //{
        //    Assert.Fail();
        //}

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
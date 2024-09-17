﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            IDisplayDialog fakeDisplayDialog = new FakeDisplayDialog();
            MainWindow mainWindow = new MainWindow(fakeDisplayDialog);
            MainWindowViewModel mainWindowVM = mainWindow.DataContext as MainWindowViewModel;
            BrowsePageViewModel browsePageVM = mainWindow.browsePage.DataContext as BrowsePageViewModel;
            //BrowsePageViewModel browsePageVM = browsePageVM.browseBooksPage.DataContext as BrowsePageViewModel;
            BrowseBooksPage browseBooksPage = browsePageVM.browseBooksPage;
            BrowseBooksPageViewModel browseBooksPageVM = browseBooksPage.DataContext as BrowseBooksPageViewModel;

            User user = new User();
            mainWindowVM.CurrentUser = user;

            PrivateObject privBrowseBooksPageVM = new PrivateObject(browseBooksPageVM);
            privBrowseBooksPageVM.Invoke("ReloadBooks"); //Selects first book 

            //browseBooksPageVM.SelectedBook = browseBooksPageVM.Books.First(); //Select first book in browse

            PrivateObject privBrowsePageVM = new PrivateObject(browsePageVM);
            privBrowsePageVM.SetField("mediaType", MediaType.Books);

            privBrowsePageVM.Invoke("Buy");


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
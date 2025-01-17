﻿using MediaKiosk.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediaKiosk.ViewModels.Browse.Tests
{
    [TestClass()]
    public class BrowseBooksPageViewModelTests
    {
        [TestMethod()]
        public void BrowseBooksPageViewModelTest()
        {
            MainWindow mainWindow = new MainWindow();
            MainWindowViewModel mainWindowVM = mainWindow.DataContext as MainWindowViewModel;
            BrowsePageViewModel browsePageViewModel = mainWindow.browsePage.DataContext as BrowsePageViewModel;
            BrowseBooksPageViewModel browseBooksVM = browsePageViewModel.browseBooksPage.DataContext as BrowseBooksPageViewModel;
            Assert.IsNotNull(browseBooksVM);
        }

        [TestMethod()]
        public void ReloadBooksTest()
        {
            MainWindow mainWindow = new MainWindow();
            MainWindowViewModel mainWindowVM = mainWindow.DataContext as MainWindowViewModel;
            BrowsePageViewModel browsePageViewModel = mainWindow.browsePage.DataContext as BrowsePageViewModel;
            BrowseBooksPageViewModel browseBooksVM = browsePageViewModel.browseBooksPage.DataContext as BrowseBooksPageViewModel;
            PrivateObject privObj = new PrivateObject(browseBooksVM);
            privObj.Invoke("ReloadBooks"); //Sets first book as SelectedBook if available

            Assert.IsNotNull(browseBooksVM.Books);

            if (mainWindowVM.MediaLibrary.Books.Count > 0)
                Assert.IsNotNull(browseBooksVM.SelectedBook);
            else
                Assert.IsNull(browseBooksVM.SelectedBook);

            mainWindowVM.MediaLibrary.Books.Add(new Models.Book());
            privObj.Invoke("ReloadBooks");

            Assert.IsNotNull(browseBooksVM.Books);
            Assert.IsNotNull(browseBooksVM.SelectedBook);
        }
    }
}
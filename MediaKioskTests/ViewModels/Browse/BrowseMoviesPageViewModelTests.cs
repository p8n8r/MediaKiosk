using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaKiosk.ViewModels.Browse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaKiosk.Views;

namespace MediaKiosk.ViewModels.Browse.Tests
{
    [TestClass()]
    public class BrowseMoviesPageViewModelTests
    {
        [TestMethod()]
        public void BrowseMoviesPageViewModelTest()
        {
            MainWindow mainWindow = new MainWindow();
            BrowsePageViewModel browsePageViewModel = mainWindow.browsePage.DataContext as BrowsePageViewModel;
            BrowseMoviesPageViewModel browseMoviesVM = browsePageViewModel.browseMoviesPage.DataContext as BrowseMoviesPageViewModel;
            Assert.IsNotNull(browseMoviesVM);
        }

        [TestMethod()]
        public void ReloadMoviesTest()
        {
            MainWindow mainWindow = new MainWindow();
            MainWindowViewModel mainWindowVM = mainWindow.DataContext as MainWindowViewModel;
            BrowsePageViewModel browsePageViewModel = mainWindow.browsePage.DataContext as BrowsePageViewModel;
            BrowseMoviesPageViewModel browseMoviesVM = browsePageViewModel.browseMoviesPage.DataContext as BrowseMoviesPageViewModel;
            PrivateObject privObj = new PrivateObject(browseMoviesVM);
            privObj.Invoke("ReloadMovies"); //Sets first movie as SelectedMovie if available

            Assert.IsNotNull(browseMoviesVM.Movies);

            if (mainWindowVM.MediaLibrary.Movies.Count > 0)
                Assert.IsNotNull(browseMoviesVM.SelectedMovie);
            else
                Assert.IsNull(browseMoviesVM.SelectedMovie);

            mainWindowVM.MediaLibrary.Movies.Add(new Models.Movie());
            privObj.Invoke("ReloadMovies");

            Assert.IsNotNull(browseMoviesVM.Movies);
            Assert.IsNotNull(browseMoviesVM.SelectedMovie);
        }
    }
}
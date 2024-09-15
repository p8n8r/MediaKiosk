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
            MainWindowViewModel mainWindowVM = new MainWindowViewModel(mainWindow);
            BrowseMoviesPageViewModel browseMoviesVM = new BrowseMoviesPageViewModel(mainWindowVM);
            Assert.IsNotNull(browseMoviesVM);
        }

        [TestMethod()]
        public void ReloadMoviesTest()
        {
            MainWindow mainWindow = new MainWindow();
            MainWindowViewModel mainWindowVM = new MainWindowViewModel(mainWindow);
            BrowseMoviesPageViewModel browseMoviesVM = new BrowseMoviesPageViewModel(mainWindowVM);
            PrivateObject privObj = new PrivateObject(browseMoviesVM);
            privObj.Invoke("ReloadMovies");

            Assert.IsNotNull(browseMoviesVM.Movies);
            Assert.IsNull(browseMoviesVM.SelectedMovie);

            mainWindowVM.MediaLibrary.Movies.Add(new Models.Movie());
            privObj.Invoke("ReloadMovies");

            Assert.IsNotNull(browseMoviesVM.Movies);
            Assert.IsNotNull(browseMoviesVM.SelectedMovie);
        }
    }
}
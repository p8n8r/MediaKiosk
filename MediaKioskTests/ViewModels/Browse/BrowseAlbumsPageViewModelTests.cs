using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaKiosk.ViewModels.Browse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using MediaKiosk.Views;

namespace MediaKiosk.ViewModels.Browse.Tests
{
    [TestClass()]
    public class BrowseAlbumsPageViewModelTests
    {
        [TestMethod()]
        public void BrowseAlbumsPageViewModelTest()
        {
            MainWindow mainWindow = new MainWindow();
            MainWindowViewModel mainWindowVM = new MainWindowViewModel(mainWindow);
            BrowseAlbumsPageViewModel browseAlbumsVM = new BrowseAlbumsPageViewModel(mainWindowVM);
            Assert.IsNotNull(browseAlbumsVM);
        }

        [TestMethod()]
        public void ReloadAlbumsTest()
        {
            MainWindow mainWindow = new MainWindow();
            MainWindowViewModel mainWindowVM = new MainWindowViewModel(mainWindow);
            BrowseAlbumsPageViewModel browseAlbumsVM = new BrowseAlbumsPageViewModel(mainWindowVM);
            PrivateObject privObj = new PrivateObject(browseAlbumsVM);
            privObj.Invoke("ReloadAlbums");

            Assert.IsNotNull(browseAlbumsVM.Albums);
            Assert.IsNull(browseAlbumsVM.SelectedAlbum);

            mainWindowVM.MediaLibrary.Albums.Add(new Models.Album());
            privObj.Invoke("ReloadAlbums");

            Assert.IsNotNull(browseAlbumsVM.Albums);
            Assert.IsNotNull(browseAlbumsVM.SelectedAlbum);
        }
    }
}
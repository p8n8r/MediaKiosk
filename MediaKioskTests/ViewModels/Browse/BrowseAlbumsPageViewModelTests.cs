using MediaKiosk.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediaKiosk.ViewModels.Browse.Tests
{
    [TestClass()]
    public class BrowseAlbumsPageViewModelTests
    {
        [TestMethod()]
        public void BrowseAlbumsPageViewModelTest()
        {
            MainWindow mainWindow = new MainWindow();
            BrowsePageViewModel browsePageViewModel = mainWindow.browsePage.DataContext as BrowsePageViewModel;
            BrowseAlbumsPageViewModel browseAlbumsVM = browsePageViewModel.browseAlbumsPage.DataContext as BrowseAlbumsPageViewModel;
            Assert.IsNotNull(browseAlbumsVM);
        }

        [TestMethod()]
        public void ReloadAlbumsTest()
        {
            MainWindow mainWindow = new MainWindow();
            MainWindowViewModel mainWindowVM = mainWindow.DataContext as MainWindowViewModel;
            BrowsePageViewModel browsePageViewModel = mainWindow.browsePage.DataContext as BrowsePageViewModel;
            BrowseAlbumsPageViewModel browseAlbumsVM = browsePageViewModel.browseAlbumsPage.DataContext as BrowseAlbumsPageViewModel;
            PrivateObject privObj = new PrivateObject(browseAlbumsVM);
            privObj.Invoke("ReloadAlbums"); //Sets first album as SelectedAlbum if available

            Assert.IsNotNull(browseAlbumsVM.Albums);

            if (mainWindowVM.MediaLibrary.Albums.Count > 0) 
                Assert.IsNotNull(browseAlbumsVM.SelectedAlbum);
            else
                Assert.IsNull(browseAlbumsVM.SelectedAlbum);

            mainWindowVM.MediaLibrary.Albums.Add(new Models.Album());
            privObj.Invoke("ReloadAlbums"); 

            Assert.IsNotNull(browseAlbumsVM.Albums);
            Assert.IsNotNull(browseAlbumsVM.SelectedAlbum);
        }
    }
}
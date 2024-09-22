using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaKiosk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaKiosk.DisplayDialogs;
using MediaKiosk.Views;
using MediaKiosk.Views.Returns;
using System.IO;
using MediaKiosk.Models;
using System.Windows.Media.Imaging;

namespace MediaKiosk.ViewModels.Tests
{
    [TestClass()]
    public class MainWindowViewModelTests
    {
        [TestMethod()]
        public void MainWindowViewModelTest()
        {
            IDisplayDialog fakeDisplayDialog = new FakeDisplayDialog();
            MainWindow mainWindow = new MainWindow(fakeDisplayDialog);
            MainWindowViewModel mainWindowVM = mainWindow.DataContext as MainWindowViewModel;

            Assert.IsNotNull(mainWindow);

            PrivateObject privReturnsPageVM = new PrivateObject(mainWindowVM);
            Assert.IsNotNull(privReturnsPageVM.GetField("displayDialog"));

            Assert.IsNotNull(mainWindowVM.Users);
            Assert.IsNotNull(mainWindowVM.MediaLibrary);
        }

        [TestMethod()]
        [DataRow(5)]
        [DataRow(11d)]
        [DataRow("hello")]
        public void ImportExportAsXmlFileTest(object obj)
        {
            IDisplayDialog fakeDisplayDialog = new FakeDisplayDialog();
            MainWindow mainWindow = new MainWindow(fakeDisplayDialog);
            MainWindowViewModel mainWindowVM = mainWindow.DataContext as MainWindowViewModel;

            string filePath = @".\Resources\example.xml";
            PrivateObject privReturnsPageVM = new PrivateObject(mainWindowVM);
            privReturnsPageVM.Invoke("ExportAsXmlFile", obj, obj.GetType(), filePath);

            Assert.IsTrue(File.Exists(filePath));

            object objReturned = privReturnsPageVM.Invoke("ImportXmlFile", obj.GetType(), filePath);

            Assert.AreEqual(obj, objReturned);
        }

        [TestMethod()]
        [DataRow(@".\Resources\example.xml")]
        public void ImportExportMediaLibraryTest(string filePath)
        {
            IDisplayDialog fakeDisplayDialog = new FakeDisplayDialog();
            MainWindow mainWindow = new MainWindow(fakeDisplayDialog);
            MainWindowViewModel mainWindowVM = mainWindow.DataContext as MainWindowViewModel;

            mainWindowVM.MediaLibrary = new MediaLibrary();

            PrivateObject privReturnsPageVM = new PrivateObject(mainWindowVM);
            privReturnsPageVM.Invoke("ExportMediaLibrary", filePath);

            Assert.IsTrue(File.Exists(filePath));

            mainWindowVM.MediaLibrary = null; //Reset

            privReturnsPageVM.Invoke("ImportMediaLibrary", filePath);

            Assert.IsNotNull(mainWindowVM.MediaLibrary);
        }

        [TestMethod()]
        [DataRow(@".\Resources\example.xml")]
        public void ImportExportUsersTest(string filePath)
        {
            IDisplayDialog fakeDisplayDialog = new FakeDisplayDialog();
            MainWindow mainWindow = new MainWindow(fakeDisplayDialog);
            MainWindowViewModel mainWindowVM = mainWindow.DataContext as MainWindowViewModel;

            mainWindowVM.Users = new List<User>();

            PrivateObject privReturnsPageVM = new PrivateObject(mainWindowVM);
            privReturnsPageVM.Invoke("ExportUsers", filePath);

            Assert.IsTrue(File.Exists(filePath));

            mainWindowVM.Users = null; //Reset

            privReturnsPageVM.Invoke("ImportUsers", filePath);

            Assert.IsNotNull(mainWindowVM.Users);
        }

        [TestMethod()]
        public void LogOutTest()
        {
            IDisplayDialog fakeDisplayDialog = new FakeDisplayDialog();
            MainWindow mainWindow = new MainWindow(fakeDisplayDialog);
            MainWindowViewModel mainWindowVM = mainWindow.DataContext as MainWindowViewModel;

            PrivateObject privReturnsPageVM = new PrivateObject(mainWindowVM);
            privReturnsPageVM.Invoke("LogOut");

            Assert.AreEqual(mainWindowVM.CurrentUser, User.INVALID_USER);
            Assert.IsFalse(mainWindowVM.HasLoggedIn);
            Assert.AreEqual((mainWindow.loginPage.DataContext as LogInPageViewModel).Username, string.Empty);
        }
    }
}
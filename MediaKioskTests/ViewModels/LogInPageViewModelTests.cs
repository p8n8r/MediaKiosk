using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaKiosk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaKiosk.Models;
using MediaKiosk.ViewModels.Browse;
using MediaKiosk.Views.Browse;
using MediaKiosk.Views;
using MediaKiosk.DisplayDialogs;
using System.Windows.Controls;

namespace MediaKiosk.ViewModels.Tests
{
    [TestClass()]
    public class LogInPageViewModelTests
    {
        [TestMethod()]
        public void LogInPageViewModelTest()
        {
            //Arrange
            IDisplayDialog fakeDisplayDialog = new FakeDisplayDialog();
            MainWindow mainWindow = new MainWindow(fakeDisplayDialog);
            MainWindowViewModel mainWindowVM = mainWindow.DataContext as MainWindowViewModel;
            LogInPageViewModel loginPageVM = mainWindow.loginPage.DataContext as LogInPageViewModel;

            Assert.IsNotNull(mainWindowVM);

            PrivateObject privLoginPageVM = new PrivateObject(loginPageVM);
            List<User> users = (List<User>)privLoginPageVM.GetField("Users");

            Assert.IsNotNull(users);
        }

        [TestMethod()]
        [DataRow("TestUser", "TestPass", false)]
        [DataRow("TestUser2", "Test Pass 2", false)]
        [DataRow("Test User 2", "TestPass2", false)]
        [DataRow("Test_User_2", "Test*Pass&2", false)]
        [DataRow("peyton", "peydey", true)]
        public void LogInTest(string username, string password, bool shouldLogIn)
        {
            //Arrange
            IDisplayDialog fakeDisplayDialog = new FakeDisplayDialog();
            MainWindow mainWindow = new MainWindow(fakeDisplayDialog);
            MainWindowViewModel mainWindowVM = mainWindow.DataContext as MainWindowViewModel;
            LogInPageViewModel loginPageVM = mainWindow.loginPage.DataContext as LogInPageViewModel;
            UserComparer comparer = new UserComparer();

            PrivateObject privMainWindowVM = new PrivateObject(mainWindowVM);
            PrivateObject privLoginPageVM = new PrivateObject(loginPageVM);
            PasswordBox passwordBox = new PasswordBox();

            loginPageVM.Username = username;
            passwordBox.Password = password;

            //Act
            privLoginPageVM.Invoke("LogIn", passwordBox);
            bool hasLoggedIn = (bool)privMainWindowVM.GetProperty("HasLoggedIn");
            User user = (User)privMainWindowVM.GetProperty("CurrentUser");

            //Assert
            if (shouldLogIn)
            {
                Assert.IsTrue(hasLoggedIn);
                Assert.AreNotEqual(user, User.INVALID_USER);
                Assert.IsTrue(mainWindowVM.Users.Any(u => u.Username == username));
            }
            else
            {
                Assert.IsFalse(hasLoggedIn);
                Assert.AreEqual(user, User.INVALID_USER);
                Assert.IsFalse(mainWindowVM.Users.Any(u => u.Username == username));
            }
        }

        [TestMethod()]
        [DataRow("TestUser", "TestPass", true)]
        [DataRow("TestUser2", "Test Pass 2", false)]
        [DataRow("Test User 2", "TestPass2", false)]
        [DataRow("Test_User_2", "Test*Pass&2", false)]
        [DataRow("peyton", "peydey", false)] 
        public void RegisterTest(string username, string password, bool shouldRegister)
        {
            //Arrange
            IDisplayDialog fakeDisplayDialog = new FakeDisplayDialog();
            MainWindow mainWindow = new MainWindow(fakeDisplayDialog);
            MainWindowViewModel mainWindowVM = mainWindow.DataContext as MainWindowViewModel;
            LogInPageViewModel loginPageVM = mainWindow.loginPage.DataContext as LogInPageViewModel;
            int countUsersInitial = mainWindowVM.Users.Count;

            PrivateObject privMainWindowVM = new PrivateObject(mainWindowVM);
            PrivateObject privLoginPageVM = new PrivateObject(loginPageVM);
            PasswordBox passwordBox = new PasswordBox();

            loginPageVM.Username = username;
            passwordBox.Password = password;

            //Act
            privLoginPageVM.Invoke("Register", passwordBox);
            bool hasLoggedIn = (bool)privMainWindowVM.GetProperty("HasLoggedIn");
            User user = (User)privMainWindowVM.GetProperty("CurrentUser");

            //Assert
            if (shouldRegister)
            {
                Assert.IsTrue(hasLoggedIn);
                Assert.AreNotEqual(user, User.INVALID_USER);
                Assert.AreEqual(countUsersInitial + 1, mainWindowVM.Users.Count);
                Assert.IsTrue(mainWindowVM.Users.Any(u => u.Username == username));
            }
            else
            {
                Assert.AreEqual(user, User.INVALID_USER);
                Assert.IsFalse(hasLoggedIn);
                Assert.AreEqual(countUsersInitial, mainWindowVM.Users.Count);
            }
        }
    }
}
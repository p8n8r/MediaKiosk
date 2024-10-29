using MediaKiosk.DisplayDialogs;
using MediaKiosk.Models;
using MediaKiosk.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace MediaKiosk.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private const string MEDIA_LIBRARY_FILE = @".\Datasets\MediaLibrary.xml";
        private const string USERS_FILE = @".\Datasets\Users.xml";
        public readonly IDisplayDialog displayDialog;

        private MainWindow mainWindow;
        public MainWindow MainWindow {  get { return mainWindow; } }
        public MediaLibrary MediaLibrary { get; set; }
        public List<User> Users { get; set; }
        public User CurrentUser { get; set; } = User.INVALID_USER;
        private bool hasLoggedIn;
        public bool HasLoggedIn
        {
            get { return hasLoggedIn; }
            set { hasLoggedIn = value; OnPropertyChanged(); }
        }
        public RelayCommand browseCmd => new RelayCommand(execute => Browse(), canExecute => this.HasLoggedIn);
        public RelayCommand returnsCmd => new RelayCommand(execute => Returns(), canExecute => this.HasLoggedIn);
        public RelayCommand donateCmd => new RelayCommand(execute => Donate(), canExecute => this.HasLoggedIn);
        public RelayCommand logOutCmd => new RelayCommand(execute => LogOut(), canExecute => this.HasLoggedIn);
        public RelayCommand onCloseCmd => new RelayCommand(execute => OnClose());
        public RelayCommand navigateToWelcomePageCmd => new RelayCommand(execute => NavigateToWelcomePage());

        public MainWindowViewModel(MainWindow mainWindow, IDisplayDialog displayDialog)
        {
            this.mainWindow = mainWindow;
            this.displayDialog = displayDialog;

            ImportUsers(USERS_FILE);
            ImportMediaLibrary(MEDIA_LIBRARY_FILE);
        }

        private void NavigateToWelcomePage()
        {
            this.mainWindow.mainFrame.Navigate(this.mainWindow.welcomePage);
        }

        private void ExportAsXmlFile(object data, Type dataType, string filePath)
        {
            FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);

            try
            {
                XmlSerializer serializer = new XmlSerializer(dataType);

                using (stream)
                {
                    serializer.Serialize(stream, data);
                }
            }
            //catch (Exception e)
            //{
            //    displayDialog.ShowErrorMessageBox(e.Message);
            //    throw; //?
            //}
            catch (ArgumentNullException e) { displayDialog.ShowErrorMessageBox(e.Message); }
            catch (ArgumentOutOfRangeException e) { displayDialog.ShowErrorMessageBox(e.Message); }
            catch (ArgumentException e) { displayDialog.ShowErrorMessageBox(e.Message); }
            catch (NotSupportedException e) { displayDialog.ShowErrorMessageBox(e.Message); }
            catch (FileNotFoundException e) { displayDialog.ShowErrorMessageBox(e.Message); }
            catch (UnauthorizedAccessException e) { displayDialog.ShowErrorMessageBox(e.Message); }
            catch (DirectoryNotFoundException e) { displayDialog.ShowErrorMessageBox(e.Message); }
            catch (PathTooLongException e) { displayDialog.ShowErrorMessageBox(e.Message); }
            catch (IOException e) { displayDialog.ShowErrorMessageBox(e.Message); }
            catch (SecurityException e) { displayDialog.ShowErrorMessageBox(e.Message); }
        }

        private object ImportXmlFile(Type dataType, string filePath)
        {
            object data = null;

            try
            {
                FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None);
                XmlSerializer serializer = new XmlSerializer(dataType);
                
                using (stream)
                {
                    data = serializer.Deserialize(stream);
                    Convert.ChangeType(data, dataType);
                }
            }
            //catch (Exception e)
            //{
            //    displayDialog.ShowErrorMessageBox(e.Message);
            //    throw; //?
            //}
            catch (ArgumentNullException e) { displayDialog.ShowErrorMessageBox(e.Message); }
            catch (ArgumentOutOfRangeException e) { displayDialog.ShowErrorMessageBox(e.Message); }
            catch (ArgumentException e) { displayDialog.ShowErrorMessageBox(e.Message); }
            catch (NotSupportedException e) { displayDialog.ShowErrorMessageBox(e.Message); }
            catch (FileNotFoundException e) { displayDialog.ShowErrorMessageBox(e.Message); }
            catch (UnauthorizedAccessException e) { displayDialog.ShowErrorMessageBox(e.Message); }
            catch (DirectoryNotFoundException e) { displayDialog.ShowErrorMessageBox(e.Message); }
            catch (PathTooLongException e) { displayDialog.ShowErrorMessageBox(e.Message); }
            catch (IOException e) { displayDialog.ShowErrorMessageBox(e.Message); }
            catch (SecurityException e) { displayDialog.ShowErrorMessageBox(e.Message); }
            catch (InvalidOperationException e) { displayDialog.ShowErrorMessageBox(e.Message); }
            catch (InvalidCastException e) { displayDialog.ShowErrorMessageBox(e.Message); }
            catch (FormatException e) { displayDialog.ShowErrorMessageBox(e.Message); }
            catch (OverflowException e) { displayDialog.ShowErrorMessageBox(e.Message); }

            return data;
        }

        private void ExportMediaLibrary(string filePath)
        {
            if (this.MediaLibrary == null) //No data found?
                this.MediaLibrary = new MediaLibrary(); //Create empty libray

            ExportAsXmlFile(this.MediaLibrary, typeof(MediaLibrary), filePath);
        }

        private void ImportMediaLibrary(string filePath)
        {
            if (File.Exists(filePath))
                this.MediaLibrary = ImportXmlFile(typeof(MediaLibrary), filePath) as MediaLibrary;

            if (this.MediaLibrary == null) //No data found?
                this.MediaLibrary = new MediaLibrary(); //Create empty library

            foreach (Book book in this.MediaLibrary.Books)
                book.ArtWork = Utility.ConvertBytesToBitmapImage(book.ArtWorkBytes);

            foreach (Album album in this.MediaLibrary.Albums)
                album.ArtWork = Utility.ConvertBytesToBitmapImage(album.ArtWorkBytes);

            foreach (Movie movie in this.MediaLibrary.Movies)
                movie.ArtWork = Utility.ConvertBytesToBitmapImage(movie.ArtWorkBytes);
        }

        private void ExportUsers(string filePath)
        {
            if (this.Users == null) //No data found?
                this.Users = new List<User>(); //Create empty users

            ExportAsXmlFile(this.Users, typeof(List<User>), filePath);
        }

        private void ImportUsers(string filePath)
        {
            if (File.Exists(filePath))
                this.Users = ImportXmlFile(typeof(List<User>), filePath) as List<User>;

            if (this.Users != null) //Data found?
            {
                foreach (User user in this.Users)
                {
                    user.Password = Cryptography.DecryptString(user.EncryptedPassword);
                }
            }
            else
            {
                this.Users = new List<User>(); //Create empty users
            }
        }

        private void Browse()
        {
            this.mainWindow.mainFrame.Navigate(this.mainWindow.browsePage);
        }

        private void Returns()
        {
            this.mainWindow.mainFrame.Navigate(this.mainWindow.returnsPage);
        }

        private void Donate()
        {
            this.mainWindow.mainFrame.Navigate(this.mainWindow.donatePage);
        }

        private void LogOut()
        {
            this.CurrentUser = User.INVALID_USER;
            this.HasLoggedIn = false;
            (this.mainWindow.loginPage.DataContext as LogInPageViewModel).Username = string.Empty;
            this.mainWindow.mainFrame.Navigate(this.mainWindow.loginPage);
        }

        private void OnClose()
        {
            ExportUsers(USERS_FILE);
            ExportMediaLibrary(MEDIA_LIBRARY_FILE);
        }
    }
}

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
    internal class MainWindowViewModel : ViewModelBase
    {
        private MainWindow mainWindow;
        private const string MEDIA_LIBRARY_FILE = @".\Datasets\MediaLibrary.xml";
        private const string USERS_FILE = @".\Datasets\Users.xml";
        internal MediaLibrary MediaLibrary { get; set; }
        internal List<User> Users { get; set; }
        private bool hasLoggedIn;
        public bool HasLoggedIn
        {
            get { return hasLoggedIn; }
            internal set { hasLoggedIn = value; OnPropertyChanged(); }
        }
        public RelayCommand browseCmd => new RelayCommand(execute => Browse(), canExecute => this.HasLoggedIn);
        public RelayCommand returnsCmd => new RelayCommand(execute => Returns(), canExecute => this.HasLoggedIn);
        public RelayCommand donateCmd => new RelayCommand(execute => Donate(), canExecute => this.HasLoggedIn);
        public RelayCommand logOutCmd => new RelayCommand(execute => LogOut(), canExecute => this.HasLoggedIn);
        public RelayCommand onCloseCmd => new RelayCommand(execute => OnClose());
        public RelayCommand navigateToWelcomePageCmd => new RelayCommand(execute => NavigateToWelcomePage());

        public MainWindowViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;

            ImportUsers();
            ImportMediaLibrary();
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
            //    Utility.ShowErrorMessageBox(e.Message);
            //    throw; //?
            //}
            catch (ArgumentNullException e) { Utility.ShowErrorMessageBox(e.Message); }
            catch (ArgumentOutOfRangeException e) { Utility.ShowErrorMessageBox(e.Message); }
            catch (ArgumentException e) { Utility.ShowErrorMessageBox(e.Message); }
            catch (NotSupportedException e) { Utility.ShowErrorMessageBox(e.Message); }
            catch (FileNotFoundException e) { Utility.ShowErrorMessageBox(e.Message); }
            catch (UnauthorizedAccessException e) { Utility.ShowErrorMessageBox(e.Message); }
            catch (DirectoryNotFoundException e) { Utility.ShowErrorMessageBox(e.Message); }
            catch (PathTooLongException e) { Utility.ShowErrorMessageBox(e.Message); }
            catch (IOException e) { Utility.ShowErrorMessageBox(e.Message); }
            catch (SecurityException e) { Utility.ShowErrorMessageBox(e.Message); }
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
            //    Utility.ShowErrorMessageBox(e.Message);
            //    throw; //?
            //}
            catch (ArgumentNullException e) { Utility.ShowErrorMessageBox(e.Message); }
            catch (ArgumentOutOfRangeException e) { Utility.ShowErrorMessageBox(e.Message); }
            catch (ArgumentException e) { Utility.ShowErrorMessageBox(e.Message); }
            catch (NotSupportedException e) { Utility.ShowErrorMessageBox(e.Message); }
            catch (FileNotFoundException e) { Utility.ShowErrorMessageBox(e.Message); }
            catch (UnauthorizedAccessException e) { Utility.ShowErrorMessageBox(e.Message); }
            catch (DirectoryNotFoundException e) { Utility.ShowErrorMessageBox(e.Message); }
            catch (PathTooLongException e) { Utility.ShowErrorMessageBox(e.Message); }
            catch (IOException e) { Utility.ShowErrorMessageBox(e.Message); }
            catch (SecurityException e) { Utility.ShowErrorMessageBox(e.Message); }
            catch (InvalidOperationException e) { Utility.ShowErrorMessageBox(e.Message); }
            catch (InvalidCastException e) { Utility.ShowErrorMessageBox(e.Message); }
            catch (FormatException e) { Utility.ShowErrorMessageBox(e.Message); }
            catch (OverflowException e) { Utility.ShowErrorMessageBox(e.Message); }

            return data;
        }

        private void ExportMediaLibrary()
        {
            if (this.MediaLibrary == null) //No data found?
                this.MediaLibrary = new MediaLibrary(); //Create empty libray

            ExportAsXmlFile(this.MediaLibrary, typeof(MediaLibrary), MEDIA_LIBRARY_FILE);
        }

        private void ImportMediaLibrary()
        {
            if (File.Exists(MEDIA_LIBRARY_FILE))
                this.MediaLibrary = ImportXmlFile(typeof(MediaLibrary), MEDIA_LIBRARY_FILE) as MediaLibrary;

            if (this.MediaLibrary == null) //No data found?
                this.MediaLibrary = new MediaLibrary(); //Create empty library

            foreach (Book book in this.MediaLibrary.Books)
                book.ArtWork = Utility.ConvertBytesToBitmapImage(book.ArtWorkBytes);

            foreach (Album album in this.MediaLibrary.Albums)
                album.ArtWork = Utility.ConvertBytesToBitmapImage(album.ArtWorkBytes);

            foreach (Movie movie in this.MediaLibrary.Movies)
                movie.ArtWork = Utility.ConvertBytesToBitmapImage(movie.ArtWorkBytes);
        }

        private void ExportUsers()
        {
            if (this.Users == null) //No data found?
                this.Users = new List<User>(); //Create empty users

            ExportAsXmlFile(this.Users, typeof(List<User>), USERS_FILE);
        }

        private void ImportUsers()
        {
            if (File.Exists(USERS_FILE))
                this.Users = ImportXmlFile(typeof(List<User>), USERS_FILE) as List<User>;

            if (this.Users != null) //Data found?
            {
                foreach (User user in this.Users)
                {
                    byte[] passwordBytes = ProtectedData.Unprotect(user.PasswordData, null,
                        DataProtectionScope.LocalMachine);

                    user.Password = Encoding.UTF8.GetString(passwordBytes);
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
            this.HasLoggedIn = false;
            this.mainWindow.mainFrame.Navigate(this.mainWindow.loginPage);
        }

        private void OnClose()
        {
            ExportUsers();
            ExportMediaLibrary();
        }

        //private void ReloadBooks(string filePath)
        //{
        //    if (File.Exists(filePath))
        //    {
        //        try
        //        {
        //            StreamReader reader = new StreamReader(filePath);
        //            string[] values;
        //            bool hasFoundHeaders = false;
        //            List<Book> books = new List<Book>();

        //            while (!reader.EndOfStream)
        //            {
        //                values = reader.ReadLine().Split(',');

        //                if (values.Length == BOOK_NUM_COLUMNS_CSV)
        //                {
        //                    if (!hasFoundHeaders)
        //                    {
        //                        hasFoundHeaders = true;
        //                        continue;
        //                    }

        //                    books.Add(new Book()
        //                    {
        //                        Title = values[BOOK_TITLE_COL],
        //                        Author = values[BOOK_AUTHORS_COL],
        //                        Category = values[BOOK_CATEGORY_COL],
        //                        PublicationDate = DateTime.Parse(values[PUBLICATION_DATE_COLUMN]),
        //                        Price = decimal.Parse(values[PRICE_COLUMN])
        //                    });
        //                }
        //            }

        //            if (books.Count > 0)
        //            {
        //                this.Books.Clear();
        //                books.ForEach(b => this.Books.Add(b)); //Copy new books over
        //            }
        //        }
        //        catch (FileNotFoundException e)
        //        {
        //            this.mainWindowViewModel.ShowErrorMessageBox(e.Message);
        //        }
        //        catch (DirectoryNotFoundException e)
        //        {
        //            this.mainWindowViewModel.ShowErrorMessageBox(e.Message);
        //        }
        //        catch (IOException e)
        //        {
        //            this.mainWindowViewModel.ShowErrorMessageBox(e.Message);
        //        }
        //    }
        //    else
        //    {
        //        string message = $"{filePath} does not exist.";
        //        this.mainWindowViewModel.ShowErrorMessageBox(message);
        //    }
        //}
    }
}
